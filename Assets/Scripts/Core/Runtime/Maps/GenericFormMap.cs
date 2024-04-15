using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Runtime.Forms;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Core.Runtime.Maps
{
    public abstract class GenericFormMap<T> : BaseFormMap where T: BaseForm
    {
        public override async Task LoadAssets(string addressableLabel)
        {
            _formDictionary = new Dictionary<string, BaseForm>();
            AsyncOperationHandle<IList<T>> handle = Addressables.LoadAssetsAsync<T>(addressableLabel, null);

            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                IList<T> loadedAssets = handle.Result;

                foreach (T form in loadedAssets)
                {
                    _formDictionary.Add(form.FormId, form);
                }
            }
            else
            {
                Debug.LogError($"Failed to load assets with label: {addressableLabel}");
            }

            Addressables.Release(handle);
        }
    }
}
