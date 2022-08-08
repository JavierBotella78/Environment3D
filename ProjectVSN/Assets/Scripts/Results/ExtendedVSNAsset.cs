using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtendedVSNAsset : VSNAsset
{
    public VSNFile[] Files_ { get; set; }
    public VSNAsset[] Sources_ { get; set; }
    public VSNAsset[] Targets_ { get; set; }
    public string CreatorName_ { get; set; }
    public string CreationTime_ { get; set; }
    public bool InRecicleBin_ { get; set; }
    public string Copyright_ { get; set; }

    public ExtendedVSNAsset(string pk, string id, string name, string imgUrl, string desc, string type, string classs, string lastDate, string creatorName, string creationTime, bool recicleBin, string copyright) : base(pk, id, name, imgUrl, desc, type, classs, lastDate)
    {
        CreatorName_ = creatorName;
        CreationTime_ = creationTime;
        InRecicleBin_ = recicleBin;
        Copyright_ = copyright;
    }

    public override string ToString()
    {
        string result = base.ToString();

        result += "\n--- Extra ---";

        result += "\nCREATOR_NAME: " + CreatorName_;
        result += "\nCREATION_TIME: " + CreationTime_;
        result += "\nIN_RECICLE_BIN: " + InRecicleBin_;
        result += "\nCOPYRIGHT: " + Copyright_;
        result += "\n\n";

        return result;
    }

}
