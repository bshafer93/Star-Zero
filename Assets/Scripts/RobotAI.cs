using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// This will be the base class for the basic robot's state machine.
/// IE, IDLE -> PATROL -> PLAYERFOUND -> ATTACK -> DIE_SADLY
/// </summary>
public class RobotAI : MonoBehaviour
{
 
  public static bool detection(Transform startingPoint,float maxDetectRange) {
      int playerMask = 1 << 6;
      playerMask = ~playerMask;
      return Physics.Raycast(startingPoint.position, PlayerInfo.player_position.position, maxDetectRange, playerMask);
  }
  public static void MoveTo(Transform goal, NavMeshAgent agent) {
      agent.destination = goal.position;
  }


    public static float getDistance(Vector3 target, Vector3 destination) {
      return Vector3.Distance(destination, target);
  }
}
