using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VSNAsset
{
    public VSNAsset()
    {
    }

    public VSNAsset(string pk, string id, string name, string imgUrl, string desc, string type, string classs, string lastDate)
    {
        PK_ = pk;
        Id_ = id;
        Name_ = name;
        ImgURL_ = imgUrl;

        Desc_ = desc;
        Type_ = type;
        Class_ = classs;
        LastDate_ = lastDate;
    }

    public string PK_ { get; }
    public string Id_ { get; }
    public string Name_ { get; }
    public string ImgURL_ { get; }
    public Texture ImgTexture_ { get; set; }

    public string Desc_ { get; }
    public string Type_ { get; }
    public string Class_ { get; }
    public string LastDate_ { get; }


    public override string ToString()
    {
        string result = "--- VSNAsset ---";

        result += "\nID: "        + Id_;
        result += "\nNAME: "    + Name_;
        result += "\nIMG_URL: " + ImgURL_;

        result += "\n\nDESCRIPTION: "   + Desc_;
        result += "\nTYPE: "            + Type_;
        result += "\nCLASS: "           + Class_;
        result += "\nLAST_DATE: "       + LastDate_;
        result += "\n\n";


        return result;
    }
}
