using System;

[AttributeUsage(AttributeTargets.Field)]
public class InjectAssetAttribute : Attribute
{

    #region Fields

    public readonly string AssetName;

    #endregion

    #region Constructors

    public InjectAssetAttribute(string assetName = null)
    {

        AssetName = assetName;

    }

    #endregion

}