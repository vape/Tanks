using System.Collections.Generic;
using Tanks.Game;
using Tanks.Game.Damage;
using Tanks.UI.Controls;
using UnityEngine;

namespace Tanks.UI
{
    public class WorldUIController : MonoBehaviour
    {
        [SerializeField]
        private HealthBar healthBarTemplate;
        [SerializeField]
        private RectTransform healthBarsContainer;

        private Canvas canvas;
        private Dictionary<int, HealthBar> healthBars = new Dictionary<int, HealthBar>();

        private void OnEnable()
        {
            canvas = GetComponentInParent<Canvas>();

            foreach (var entity in World.Damage.Entities)
            {
                OnEntityRegistered(entity);
            }

            World.Damage.EntityRegistered += OnEntityRegistered;
            World.Damage.EntityUnregistered += OnEntityUnregistered;
        }

        private void OnDisable()
        {
            foreach (var entity in World.Damage.Entities)
            {
                OnEntityUnregistered(entity);
            }

            World.Damage.EntityRegistered -= OnEntityRegistered;
            World.Damage.EntityUnregistered -= OnEntityUnregistered;
        }

        private void OnEntityRegistered(IDamagableEntity entity)
        {
            var gameObjectEntity = entity as IDamagableGameObject;
            if (gameObjectEntity == null)
            {
                return;
            }

            if (healthBars.TryGetValue(entity.Id, out _))
            {
                Debug.LogWarning($"Trying to register entity {gameObjectEntity.GameObject.name}:{entity.Id} twice");
                OnEntityUnregistered(entity);
            }

            var bar = Instantiate(healthBarTemplate, healthBarsContainer);
            bar.SetTarget(canvas.worldCamera, gameObjectEntity);

            healthBars[entity.Id] = bar;
        }

        private void OnEntityUnregistered(IDamagableEntity entity)
        {
            if (healthBars.Remove(entity.Id, out var bar))
            {
                Destroy(bar.gameObject);
            }
        }
    }
}
