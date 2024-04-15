using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Runtime.Forms;
using UnityEngine;

namespace Core.Runtime.Maps
{
    public abstract class BaseFormMap : MonoBehaviour
    {
        protected Dictionary<string, BaseForm> _formDictionary;

        public Dictionary<string, BaseForm> FormDictionary => _formDictionary;


        public abstract Task LoadAssets(string addressableLabel);
    }
}
