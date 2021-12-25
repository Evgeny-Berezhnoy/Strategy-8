using System;
using UnityEngine;

using Object = UnityEngine.Object;

[CreateAssetMenu(fileName = nameof(AssetsContext), menuName = "Strategy Game/" + nameof(AssetsContext), order = 0)]
public class AssetsContext : ScriptableObject
{

    #region Fields

    [SerializeField] private Object[] _objects;

    #endregion

    #region Methods

    public Object GetObjectOfType(Type targetType, string targetName = null)
    {

        for(int i = 0; i < _objects.Length; i++)
        {

            var currentObject = _objects[i];

            if(currentObject.GetType().IsAssignableFrom(targetType)
                && (targetName == null
                       || currentObject.name == targetName))
            {

                return currentObject;

            };

        };

        return null;

    }

    #endregion

}
