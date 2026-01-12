using System.Collections;
using _Project._Code.Core.Gird;
using UnityEngine;

namespace _Project._Code.Core
{
    public class Spawner : MonoBehaviour, IGridObject
    {
        public Spawner()
        {
            
        }
        
        public void Activate()
        {
            Debug.Log("Activate Spawner");
        }
    }
}