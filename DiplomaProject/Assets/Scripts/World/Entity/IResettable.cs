using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.Entity
{
    public interface IResettable
    {
        void ResetState();
        void Activate();
    }
}
