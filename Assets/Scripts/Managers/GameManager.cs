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
        private bool _isPlayerClimbing;

        private void Start()
        {
            _player = GameObject.FindWithTag("Player").GetComponent<IClimberPlayer>();
            _player.OnPlayerClimbed += PlayerClimbedHandler;
            _player.OnPlayerCantReached += PlayerCantReachedHandler;
            _player.OnFatalDamage += PlayerFatalDamageHandler;
            ClimbItemClickInfoProvider.OnItemClick += ClimbItemSelectedHandler;
        }

        private void OnDestroy()
        {
            ClimbItemClickInfoProvider.OnItemClick -= ClimbItemSelectedHandler;
            _player.OnPlayerClimbed -= PlayerClimbedHandler;
            _player.OnPlayerCantReached -= PlayerCantReachedHandler;
            _player.OnFatalDamage -= PlayerFatalDamageHandler;
        }

        private void ClimbItemSelectedHandler(IClimbItem climbItem)
        {
            if(_isPlayerClimbing) return;
            _player.TryReachTarget(climbItem);
            _currentClimbItem = climbItem;
            _isPlayerClimbing = true;
        }

        private void PlayerClimbedHandler(IClimberPlayer player)
        {
            TryReleasePlayerOnClimbing();
            _currentClimbItem.InteractWithThePlayer(_player);
            _isPlayerClimbing = false;
        }

        private void PlayerCantReachedHandler(IClimberPlayer player)
        {
            print("player cant reach");
            _isPlayerClimbing = false;
        }

        private void PlayerFatalDamageHandler()
        {
            print("player fatal damage");
            print("Game over!!!");
            TryReleasePlayerOnClimbing();
            ReleaseCurrentItem();
        }

        private void TryReleasePlayerOnClimbing()
        {
            if (_previousClimbItem != null && _previousClimbItem != _currentClimbItem)
            {
                _previousClimbItem.ReleasePlayer(_player);
                _previousClimbItem = _currentClimbItem;
            }

            _previousClimbItem ??= _currentClimbItem;
        }

        private void ReleaseCurrentItem()
        {
            _currentClimbItem.ReleasePlayer(_player);
        }
    }
}