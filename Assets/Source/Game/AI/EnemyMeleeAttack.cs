using System;
using UnityEngine;

namespace Tanks.Game.AI
{
    public class EnemyMeleeAttack : MonoBehaviour
    {
        public enum AttackPhase
        {
            Idle,
            Charging,
            Performing,
            Reloading
        }

        [Serializable]
        public struct AttackDescriptor
        {
            public float Damage;
            public float ChargeTime;
            public float AttackTime;
            public float ReloadTime;
        }

        public const string DamageAreaTag = "DamageArea";

        public AttackPhase Phase => phase;
        public AttackDescriptor ActiveAttack => attacks[attackIndex];

        [SerializeField]
        private AttackDescriptor[] attacks;

        private int attackIndex;
        private float elapsedPhase;
        private AttackPhase phase;
        private bool atMeleeRange;
        private GameObject target;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == DamageAreaTag)
            {
                target = other.gameObject;
                atMeleeRange = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == DamageAreaTag)
            {
                target = null;
                atMeleeRange = false;
            }
        }

        private void Update()
        {
            elapsedPhase += Time.deltaTime;

            switch (phase)
            {
                case AttackPhase.Idle:
                    if (atMeleeRange)
                    {
                        SelectRandomAttack();
                        SetPhase(AttackPhase.Charging);
                    }
                    break;

                case AttackPhase.Charging:
                    if (!atMeleeRange)
                    {
                        SetPhase(AttackPhase.Idle);
                    }
                    else if (elapsedPhase >= ActiveAttack.ChargeTime)
                    {
                        SetPhase(AttackPhase.Performing);
                    }
                    break;

                case AttackPhase.Performing:
                    if (elapsedPhase >= ActiveAttack.AttackTime)
                    {
                        ApplyDamage();
                        SetPhase(AttackPhase.Reloading);
                    }
                    break;

                case AttackPhase.Reloading:
                    if (elapsedPhase >= ActiveAttack.ReloadTime)
                    {
                        SetPhase(AttackPhase.Idle);
                    }
                    break;
            }
        }

        private void SetPhase(AttackPhase phase)
        {
            this.phase = phase;
            this.elapsedPhase = 0;
        }

        private void ApplyDamage()
        {
            if (atMeleeRange && target != null)
            {
                World.Damage.Damage(target, ActiveAttack.Damage, new Damage.DamageInfo() { Source = gameObject });
            }
        }
        
        private void SelectRandomAttack()
        {
            attackIndex = UnityEngine.Random.Range(0, attacks.Length);
        }
    }
}
