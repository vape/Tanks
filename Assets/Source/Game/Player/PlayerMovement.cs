using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Tanks.Game.Player
{
    public abstract class PlayerMovement : MonoBehaviour
    {
        public abstract void SetPositionImmediately(Vector3 position);
    }
}
