using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TrackLoader : MonoBehaviour {
    public float trackTime;
    public float timeLeft;
    private int currentTrack = 0;
    public bool started = false;
    private int trackManIterations = 0;
    public int neatIterations = 0;
    public FU2POP trackPopulation;

    public GameObject[] trackPrefabs;
    List<GameObject> currentTrackSegements = new List<GameObject>();

	public void Awake()
	{
        trackPopulation = new FU2POP(15);

        loadTrack(trackPopulation.feasable[currentTrack].GetGenes());
	}


	private void FixedUpdate()
	{
        if (!started) return;
        if (trackManIterations > neatIterations) return;
        timeLeft -= Time.deltaTime;
        if(timeLeft <= 0)
        {
            currentTrack++;
            updateCarFitness();
            updateTrackFitness();

            timeLeft = trackTime;
            if(currentTrack >= trackPopulation.feasable.Count)
            {
                //if currentTrack > length of track pop then generate next pop 
                currentTrack = 0;
            } else {
                loadTrack(trackPopulation.feasable[currentTrack].GetGenes());
            }
            trackManIterations++;
        }
	}

    private void updateCarFitness()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Car");
        foreach(GameObject obj in objects)
        {
            obj.GetComponent<CarController>().storeCurrentTrackFitness();
        }
        
    }

    private void updateTrackFitness()
    {

    }

    private void loadTrack(int[] track)
    {
        //0 = hard right, 1 = hard left, 2 = soft right, 3 = soft left, 4 = straight, 5 = end
        //remove old track
        for (int i = 0; i < currentTrackSegements.Count; i++)
        {
            Destroy(currentTrackSegements[i]);
        }
        currentTrackSegements.Clear();

        List<int[]> coords = new List<int[]>();

        int entryDir = 0;

        int[] startingCoords = { 0, 0 };
        coords.Add(startingCoords);

        foreach(int trackElemt in track)
        {
            int[] latestCoords = coords[coords.Count-1];
            Quaternion rotation = new Quaternion();
            if(entryDir == 0) rotation = Quaternion.Euler(0f, 0f, 0f);
            else if (entryDir == 1) rotation = Quaternion.Euler(0f,90f,0f);
            else if (entryDir == 2) rotation = Quaternion.Euler(0f, 180f, 0f);
            else if (entryDir == 3) rotation = Quaternion.Euler(0f,270f, 0f);

            GameObject newTrack = Instantiate(trackPrefabs[trackElemt], new Vector3(latestCoords[0]*10, 0, latestCoords[1]*10), rotation);
            currentTrackSegements.Add(newTrack);

            int[] nextCoords = new int[2];

            if(entryDir == 0)
            {
                if (trackElemt == 0 || trackElemt == 2)
                {
                    nextCoords[0] = latestCoords[0] + 1;
                    nextCoords[1] = latestCoords[1];
                    entryDir = 1;
                }
                else if(trackElemt == 1 || trackElemt == 3)
                {
                    nextCoords[0] = latestCoords[0] - 1;
                    nextCoords[1] = latestCoords[1];
                    entryDir = 3;
                    
                } else if (trackElemt == 4 || trackElemt == 5)
                {
                    nextCoords[0] = latestCoords[0];
                    nextCoords[1] = latestCoords[1]+1;
                    entryDir = 0;
                }
            } else if(entryDir == 1)
            {
                if (trackElemt == 0 || trackElemt == 2)
                {
                    nextCoords[0] = latestCoords[0];
                    nextCoords[1] = latestCoords[1]-1;
                    entryDir = 2;
                }
                else if (trackElemt == 1 || trackElemt == 3)
                {
                    nextCoords[0] = latestCoords[0];
                    nextCoords[1] = latestCoords[1]+1;
                    entryDir = 0;

                }
                else if (trackElemt == 4 || trackElemt == 5)
                {
                    nextCoords[0] = latestCoords[0]+1;
                    nextCoords[1] = latestCoords[1];
                    entryDir = 1;
                }
                
            } else if (entryDir == 2)
            { 
                if (trackElemt == 0 || trackElemt == 2)
                {
                    nextCoords[0] = latestCoords[0] -1;
                    nextCoords[1] = latestCoords[1];
                    entryDir = 3;
                }
                else if (trackElemt == 1 || trackElemt == 3)
                {
                    nextCoords[0] = latestCoords[0] + 1;
                    nextCoords[1] = latestCoords[1];
                    entryDir = 1;

                }
                else if (trackElemt == 4 || trackElemt == 5)
                {
                    nextCoords[0] = latestCoords[0];
                    nextCoords[1] = latestCoords[1]-1;
                    entryDir = 2;
                }
            }else if (entryDir == 3)
            { 
                if (trackElemt == 0 || trackElemt == 2)
                {
                    nextCoords[0] = latestCoords[0];
                    nextCoords[1] = latestCoords[1]+1;
                    entryDir = 0;
                }
                else if (trackElemt == 1 || trackElemt == 3)
                {
                    nextCoords[0] = latestCoords[0];
                    nextCoords[1] = latestCoords[1]-1;
                    entryDir = 2;

                }
                else if (trackElemt == 4 || trackElemt == 5)
                {
                    nextCoords[0] = latestCoords[0]-1;
                    nextCoords[1] = latestCoords[1];
                    entryDir = 3;
                }
            }
            coords.Add(nextCoords);
        }
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Car");
        foreach (GameObject obj in objects)
        {
            obj.transform.position = Vector3.zero;
            obj.transform.rotation = Quaternion.identity;
        }
    }

    public static bool Check( TrackChromosome t)
    {
        int[] track = t.GetGenes();
        //0 = hard right, 1 = hard left, 2 = soft right, 3 = soft left, 4 = straight, 5 = end
        //remove old track

        List<int[]> coords = new List<int[]>();

        int entryDir = 0;

        int[] startingCoords = { 0, 0 };
        coords.Add(startingCoords);

        foreach (int trackElemt in track)
        {
            int[] latestCoords = coords[coords.Count - 1];
            int[] nextCoords = new int[2];

            if (entryDir == 0)
            {
                if (trackElemt == 0 || trackElemt == 2)
                {
                    nextCoords[0] = latestCoords[0] + 1;
                    nextCoords[1] = latestCoords[1];
                    entryDir = 1;
                }
                else if (trackElemt == 1 || trackElemt == 3)
                {
                    nextCoords[0] = latestCoords[0] - 1;
                    nextCoords[1] = latestCoords[1];
                    entryDir = 3;

                }
                else if (trackElemt == 4 || trackElemt == 5)
                {
                    nextCoords[0] = latestCoords[0];
                    nextCoords[1] = latestCoords[1] + 1;
                    entryDir = 0;
                }
            }
            else if (entryDir == 1)
            {
                if (trackElemt == 0 || trackElemt == 2)
                {
                    nextCoords[0] = latestCoords[0];
                    nextCoords[1] = latestCoords[1] - 1;
                    entryDir = 2;
                }
                else if (trackElemt == 1 || trackElemt == 3)
                {
                    nextCoords[0] = latestCoords[0];
                    nextCoords[1] = latestCoords[1] + 1;
                    entryDir = 0;

                }
                else if (trackElemt == 4 || trackElemt == 5)
                {
                    nextCoords[0] = latestCoords[0] + 1;
                    nextCoords[1] = latestCoords[1];
                    entryDir = 1;
                }

            }
            else if (entryDir == 2)
            {
                if (trackElemt == 0 || trackElemt == 2)
                {
                    nextCoords[0] = latestCoords[0] - 1;
                    nextCoords[1] = latestCoords[1];
                    entryDir = 3;
                }
                else if (trackElemt == 1 || trackElemt == 3)
                {
                    nextCoords[0] = latestCoords[0] + 1;
                    nextCoords[1] = latestCoords[1];
                    entryDir = 1;

                }
                else if (trackElemt == 4 || trackElemt == 5)
                {
                    nextCoords[0] = latestCoords[0];
                    nextCoords[1] = latestCoords[1] - 1;
                    entryDir = 2;
                }
            }
            else if (entryDir == 3)
            {
                if (trackElemt == 0 || trackElemt == 2)
                {
                    nextCoords[0] = latestCoords[0];
                    nextCoords[1] = latestCoords[1] + 1;
                    entryDir = 0;
                }
                else if (trackElemt == 1 || trackElemt == 3)
                {
                    nextCoords[0] = latestCoords[0];
                    nextCoords[1] = latestCoords[1] - 1;
                    entryDir = 2;

                }
                else if (trackElemt == 4 || trackElemt == 5)
                {
                    nextCoords[0] = latestCoords[0] - 1;
                    nextCoords[1] = latestCoords[1];
                    entryDir = 3;
                }
            }
            foreach (int[] prevCoord in coords)
            {
                if (prevCoord[0] == nextCoords[0] && prevCoord[1] == nextCoords[1]) return false;

            }
            coords.Add(nextCoords);
        }
        return true;
    }


}
