using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class sample : MonoBehaviour
{
   public IDictionary<Vector3, Vector3> dict = new Dictionary<Vector3, Vector3>();
    static List<vectorwrapper> Explored = new List<vectorwrapper>();
    public List<vectorwrapper> Adjacents = new List<vectorwrapper>();
    public vectorwrapper Begin;
    public vectorwrapper End;

     public void findpath(vectorwrapper start ,vectorwrapper end)
      {
        List<Vector3> path = new List<Vector3>();
        path.Add(end.vector);
        Vector3 sum = end.vector;
        while (sum != start.vector)
        {
            path.Add(dict[sum]);
            sum = dict[sum];
        }
        for(int i = 0; i< path.Count; i++)
        {
            Debug.Log(path[i]);
        }
         
        
         foreach(Vector3 g in path)
          {
              GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
              sphere.GetComponent<Renderer>().material.color = Color.cyan;
              sphere.transform.position = new Vector3(g.x, 1.5f, g.z);
              sphere.transform.localScale = new Vector3(3, 3, 3);
          }
      } 
	public void GetAdjacents(vectorwrapper input) // function to fetch the neighbouring nodes
	{
        Adjacents.Clear();
		float x = input.vector.x;
		float z = input.vector.z;

		float[] set = { x - 10.0f, z - 10.0f, x - 10.0f, z, x - 10.0f, z + 10.0f, x, z - 10.0f, x, z + 10.0f, x + 10.0f, z - 10.0f, x + 10.0f, z, x + 10.0f, z + 10.0f };
		int j = 0;
        vectorwrapper Adjacent;
		for (int i = 0; i < 8; i++)
		{
            Adjacent = new vectorwrapper(new Vector3(set[j], 0.5f, set[j + 1]));
            if (Adjacent.vector.x >= 0 && Adjacent.vector.x <= 90)
			{
				if (Adjacent.vector.z >= 0 && Adjacent.vector.z <= 90)
				{
					Adjacents.Add(Adjacent);
				}
			}
			j = j + 2;
		}
	}
     public void Start()
    {
        List<Vector3> openvectors = new List<Vector3>();
        List<Vector3> closedvectors = new List<Vector3>(); 
         Vector3 v1 = new Vector3(0, 0.5f,0);
         Vector3 v2 = new Vector3(0, 0.5f, 80);
         Begin = new vectorwrapper(v1);
         End = new vectorwrapper(v2);
         List<vectorwrapper> Frontier = new List<vectorwrapper>();
         Frontier.Add(Begin);
         Begin.hcost = Vector3.Distance(Begin.vector, End.vector);
          while(Frontier.Count > 0)
         {
             vectorwrapper current = Frontier[0];
             for (int i = 1; i < Frontier.Count; i++)
             {
                 if ((Frontier[i].TotalCost < current.TotalCost) || ((Frontier[i].TotalCost == current.TotalCost) && (Frontier[i].hcost < current.hcost)))
                 {
                     current = Frontier[i];
                 }
             }
             if (current.vector == End.vector)
             {
                //Debug.Log("current's parent " + current.parent);
                //Debug.Log("End's parent " + End.parent);
                findpath(Begin,current);
                // Debug.Log("Found");
                 return;
             }
            Explored.Add(current);
            closedvectors.Add(current.vector);
            Frontier.Remove(current);
            GetAdjacents(current);
            foreach (vectorwrapper neighbour in Adjacents)
             {
                if (closedvectors.Contains(neighbour.vector))
                     continue;

              float tentative_gcost = current.gcost + Vector3.Distance(current.vector,neighbour.vector) + Terrain.activeTerrain.SampleHeight(neighbour.vector);
                
                 if (!Frontier.Contains(neighbour))
                 {
                     Frontier.Add(neighbour);
                    
                 }
                 else if (tentative_gcost >= neighbour.gcost)
                          continue;

                 dict[neighbour.vector] = current.vector;
                 neighbour.gcost = tentative_gcost;
                 neighbour.hcost = Vector3.Distance(neighbour.vector, End.vector);
                 }
         
                 }
                 
        
        

        } 
}