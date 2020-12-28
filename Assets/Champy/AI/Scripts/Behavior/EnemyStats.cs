using UnityEngine;

namespace Champy.AI
{
    [CreateAssetMenu(menuName = "Champy2/AI/Enemy Stats")]
    public class EnemyStats : ScriptableObject
    {
        public float moveSpeed = 1;
        public float lookRange = 40f;
        public float lookSphereCastRadius = 1f;

        public float attackRange = 1f;
        public float attackRate = 1f;
        public float attackForce = 15f;
        public int attackDamage = 50;

        public float searchDuration = 4f;
        public float searchingTurnSpeed = 120f;
    }
}