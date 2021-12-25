using System;
using System.Reflection;

public static class AssetsInjector
{

    #region Fields

    private static readonly Type _injectAssetAttributeType = typeof(InjectAssetAttribute);

    #endregion

    #region Methods

    public static T Inject<T>(this AssetsContext context, T target)
    {

        for(Type targetType = target.GetType(); targetType != typeof(object); targetType = targetType.BaseType)
        {

            var allFields = targetType.GetFields(
                                    BindingFlags.Public |
                                    BindingFlags.NonPublic |
                                    BindingFlags.Instance);

            for (int i = 0; i < allFields.Length; i++)
            {

                var fieldInfo = allFields[i];
                var injectAssetAttribute = fieldInfo.GetCustomAttribute(_injectAssetAttributeType) as InjectAssetAttribute;

                if (injectAssetAttribute == null) continue;

                var objectToInject = context.GetObjectOfType(fieldInfo.FieldType, injectAssetAttribute.AssetName);

                fieldInfo.SetValue(target, objectToInject);

            };

        }

        return target;

    }

    #endregion

}