using Main.Scripts.Configs.Boosts;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services.Collision;
using UnityEngine;

namespace Main.Scripts.Logic.Balls
{
    public class Bullet : MonoBehaviour, ITriggerInteractable
    {
        private Ball _ball;
        private ITimeProvider _timeProvider;

        private Vector2 _currentPosition;
        private Vector2 _currentVelocity;

        private bool _destroyed;

        public MachineGunConfig MachineGunConfig { get; private set; }

        public void Construct(Ball ball, ITimeProvider timeProvider)
        {
            _ball = ball;
            _timeProvider = timeProvider;
            _currentPosition = transform.position;
            _destroyed = false;
        }
        
        public void StartMove(MachineGunConfig machineGunConfig)
        {
            MachineGunConfig = machineGunConfig;
            _currentVelocity = Vector2.up * MachineGunConfig.BulletSpeed;
        }
        
        private void Update()
        {
            _currentPosition += _currentVelocity * _timeProvider.DeltaTime;
            transform.position = _currentPosition;
        }

        public void Interact()
        {
            if (_destroyed)
            {
                return;
            }
            _destroyed = true;
            _ball.Destroy();
        }
    }
}