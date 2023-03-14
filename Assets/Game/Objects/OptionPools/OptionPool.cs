using Assets.Game.Extensions;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Objects.OptionPools
{
    public class OptionPool : MonoBehaviour, ISelectable<string>
    {
        [SerializeField] private List<string> options;
        
        public string ChooseOne()
        {
            return options.RandomElement();
        }
    }
}