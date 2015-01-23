﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class POI
{
    [XmlElement("SceneFlag")]
    public string sceneFlag;

    [XmlElement("Name")]
    public string buttonName;

    [XmlElement("Position")]
    public Vector3 position;

    [XmlElement("Rotation")]
    public Vector3 rotation;

    public POI()
    {
    }

    public POI(string sFlag, string bName, Vector3 pos, Vector3 rot)
    {
        sceneFlag = sFlag;
        buttonName = bName;
        position = pos;
        rotation = rot;
    }
}
[XmlRoot]
public class POIHandler
{
    [XmlArray("ProjectPOIs")]
    [XmlArrayItem("ScenePOIs")]
    public List<List<POI>> projectPOIs;

    public void AddPoint(POI point)
    {
        // check if an existing list has the scene flag
        // if not, create a new list for the point
        bool matchesExisting = false;
        
        foreach (List<POI> sceneList in projectPOIs)
        {
            if (sceneList[0].sceneFlag == point.sceneFlag)
            {
                sceneList.Add(point);
                matchesExisting = true;
                break;
            }
        }

        if(!matchesExisting)
        {
            List<POI> newList = new List<POI>();
            newList.Add(point);
            projectPOIs.Add(newList);
        }

    }

	public void RemovePoint(POI point)
	{
		foreach(List<POI> sceneList in projectPOIs)
		{
			if(sceneList[0].sceneFlag == point.sceneFlag)
			{
				foreach(POI scenePoint in sceneList)
				{
					if(point == scenePoint)
					{
						sceneList.Remove(point);
						if(sceneList.Count == 0)
							projectPOIs.Remove(sceneList);
						break;
					}
				}
			}
		}
	}

	public void UpdatePoint(POI oldPoint, POI newPoint)
	{
		foreach(List<POI> sceneList in projectPOIs)
		{
			if(sceneList[0].sceneFlag == oldPoint.sceneFlag)
			{
				for(int i = 0; i < sceneList.Count;i++)
				{
					if(oldPoint == sceneList[i])
					{
						sceneList[i] = newPoint;
						break;
					}
				}
			}
		}
	}

    public POIHandler()
    {
        projectPOIs = new List<List<POI>>();
    }
}