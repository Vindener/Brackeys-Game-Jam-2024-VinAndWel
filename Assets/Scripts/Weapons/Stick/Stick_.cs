using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Stick_ : MonoBehaviour
    {
        public event EventHandler OnStickSwing;


        private PolygonCollider2D polygonCollider2D;

        [SerializeField] private int damage = 2;

        private void Awake()
        {
            polygonCollider2D = GetComponent<PolygonCollider2D>();
        }

        private void Start()
        {
            AttackColliderTurnOff();
        }

        public void Attack()
        {
            AttackColliderTurnOnOff();

            OnStickSwing?.Invoke(this, EventArgs.Empty);

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.TryGetComponent(out EnemyEntity enemyEntity))
            {
                enemyEntity.TakeDamage(damage);
            }
        }

        public void AttackColliderTurnOff()
        {
            polygonCollider2D.enabled = false;
        }

        private void AttackColliderTurnOn()
        {
            polygonCollider2D.enabled = true;
        }

        private void AttackColliderTurnOnOff()
        {
            AttackColliderTurnOff();
            AttackColliderTurnOn();
        }
    }
