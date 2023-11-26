using Main.Scripts.Configs.Boosts;
using Main.Scripts.Infrastructure.Provides;
using Main.Scripts.Infrastructure.Services.Collision;
using Main.Scripts.Logic.Balls.BallContainers;
using UnityEngine;

namespace Main.Scripts.Logic.Balls
{
    public class Bullet : MonoBehaviour, ITriggerInteractable
    {
        private Ball _ball;
        private IBallContainer _ballContainer;
        private ITimeProvider _timeProvider;

        private Vector2 _currentPosition;
        private Vector2 _currentVelocity;

        private float _invulnerabilityTime = 0.3f;
        public MachineGunConfig MachineGunConfig { get; private set; }

        public void Construct(Ball ball, IBallContainer ballContainer, ITimeProvider timeProvider)
        {
            _ball = ball;
            _ballContainer = ballContainer;
            _timeProvider = timeProvider;
            _currentPosition = transform.position;
        }
        
        public void StartMove(MachineGunConfig machineGunConfig)
        {
            MachineGunConfig = machineGunConfig;
            _invulnerabilityTime = 0.6f / MachineGunConfig.BulletSpeed; 
            _currentVelocity = Vector2.up * MachineGunConfig.BulletSpeed;
        }
        
        private void Update()
        {
            if (_invulnerabilityTime > 0f)
            {
                _invulnerabilityTime -= _timeProvider.DeltaTime;
            }
            
            _currentPosition += _currentVelocity * _timeProvider.DeltaTime;
            transform.position = _currentPosition;
        }

        public void Interact()
        {
            if (_invulnerabilityTime > 0f)
            {
                return;
            }
            
            _ballContainer.RemoveBall(_ball);
        }
    }
}