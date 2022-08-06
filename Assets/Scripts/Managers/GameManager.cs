using ClickInfo;
using ClimbItem;
using Player;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        private IClimberPlayer _player;
        private IClimbItem _currentClimbItem;
        private IClimbItem _previousClimbItem;

        private void Start()
        {
            _player = GameObject.FindWithTag("Player").GetComponent<IClimberPlayer>();
            _player.OnPlayerClimbed += PlayerClimbedHandler;
            _player.OnPlayerCantReached += PlayerCantReachedHandler;
            ClimbItemClickInfoProvider.OnItemClick += ClimbItemSelectedHandler;
        }

        private void OnDestroy()
        {
            ClimbItemClickInfoProvider.OnItemClick -= ClimbItemSelectedHandler;
            _player.OnPlayerClimbed -= PlayerClimbedHandler;
            _player.OnPlayerCantReached -= PlayerCantReachedHandler;
        }

        private void ClimbItemSelectedHandler(IClimbItem climbItem)
        {
            print("climb item selected.");
            _player.TryReachTarget(climbItem.ClimbTargetTransforms);
            _currentClimbItem = climbItem;
            
        }

        private void PlayerClimbedHandler(IClimberPlayer player)
        {
            print("player climbed");
            TryReleasePlayer();
            _currentClimbItem.InteractWithThePlayer(_player);
        }

        private void PlayerCantReachedHandler(IClimberPlayer player)
        {
            print("player cant reach");
        }

        private void TryReleasePlayer()
        {
            if (_previousClimbItem != null && _previousClimbItem != _currentClimbItem)
            {
                _previousClimbItem.ReleasePlayer(_player);
                _previousClimbItem = _currentClimbItem;
            }

            _previousClimbItem ??= _currentClimbItem;
        }
    }
}