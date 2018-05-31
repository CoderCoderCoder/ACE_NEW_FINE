using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TrackLoader : MonoBehaviour {
    public float trackTime;
    public float timeLeft;
    private int currentTrack = 0;
    public bool started = false;

	private void FixedUpdate()
	{
        if (!started) return;
        timeLeft -= Time.deltaTime;
        if(timeLeft <= 0)
        {
            currentTrack++;
            updateCarFitness();
            updateTrackFitness();

            timeLeft = trackTime;
            //if currentTrack > length of track pop then generate next pop 
            //else ...
            loadTrack(currentTrack);
        }
	}

    private void updateCarFitness()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Car");
        print(objects.Length);
        foreach(GameObject obj in objects)
        {
            obj.GetComponent<CarController>().storeCurrentTrackFitness();
        }
        
    }

    private void updateTrackFitness()
    {

    }

    private void loadTrack(int track)
    {
        return;
    }

}
