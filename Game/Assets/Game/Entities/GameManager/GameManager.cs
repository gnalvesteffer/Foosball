using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private enum Team { Red, Blue, None };

    private Team _lastTeamToDropBall = Team.None;

    public float MinDropSpeed = 0.5f;
    public float MaxDropSpeed = 5.0f;
    public Ball Ball;
    public Collider2D RedGoalTrigger;
    public Collider2D BlueGoalTrigger;
    public Transform RedBallDropTransform;
    public Transform BlueBallDropTransform;
    public Transform CenterTransform;
    public ScoreIndicator RedScoreIndicator;
    public ScoreIndicator BlueScoreIndicator;
    public Rod[] RedRods;
    public Rod[] BlueRods;

    private Team TeamWithBallInGoal
    {
        get
        {
            if(RedGoalTrigger.bounds.Contains(Ball.transform.position))
            {
                return Team.Blue;
            }
            else if(BlueGoalTrigger.bounds.Contains(Ball.transform.position))
            {
                return Team.Red;
            }
            return Team.None;
        }
    }

    private void Start()
	{
        StartCoroutine(Initialize());
        StartCoroutine(GameLoop());        
    }

    private IEnumerator Initialize()
    {
        yield return new WaitForFixedUpdate();
        RedScoreIndicator.Score = 0;
        BlueScoreIndicator.Score = 0;
        StartCoroutine(ServeBall(Team.Red));
        StartCoroutine(StuckBallMonitor());
    }

    private IEnumerator StuckBallMonitor()
    {
        while(true)
        {
            yield return new WaitUntil(() => Ball.RigidBody2D.velocity.magnitude <= 0.1f);
            yield return new WaitForSeconds(3);
            if (Ball.RigidBody2D.velocity.magnitude <= 0.1f)
            {
                StartCoroutine(ServeBall(_lastTeamToDropBall));
            }
        }
    }

    private IEnumerator GameLoop()
    {
        while (true)
        {
            switch (TeamWithBallInGoal)
            {
                case Team.Red:
                    ++RedScoreIndicator.Score;
                    StartCoroutine(ServeBall(Team.Blue));
                    break;
                case Team.Blue:
                    ++BlueScoreIndicator.Score;
                    StartCoroutine(ServeBall(Team.Red));
                    break;
                case Team.None:
                    break;
            }

            if (RedScoreIndicator.Score == 8 || BlueScoreIndicator.Score == 8)
            {
                yield return Initialize();
            }

            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator ServeBall(Team servingTeam)
    {
        if (servingTeam == Team.None) yield return null;

        _lastTeamToDropBall = servingTeam;

        Ball.transform.position = servingTeam == Team.Red ? RedBallDropTransform.position : BlueBallDropTransform.position;
        Ball.RigidBody2D.simulated = false;
        yield return new WaitForSeconds(1.25f);
        Ball.RigidBody2D.simulated = true;

        var dropTargetPosition = CenterTransform.position + new Vector3(Random.value * 2.0f - 1.0f, Random.value * 2.0f - 1.0f) * 0.05f;
        var dropVelocity = (Ball.transform.position - dropTargetPosition).normalized * (MinDropSpeed + Random.value * (MaxDropSpeed - MinDropSpeed));
        Ball.RigidBody2D.velocity = dropVelocity;
    }
}
