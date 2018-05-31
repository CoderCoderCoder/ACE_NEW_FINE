using UnityEngine;
using System.Collections;
using SharpNeat.Phenomes;
using System.Collections.Generic;

public class CarController : UnitController {
    public SimpleEvaluator evaluator;
    public float Speed = 5f;
    public float TurnSpeed = 180f;
    public int Lap = 1;
    public int CurrentPiece, LastPiece;
    public float progress;
    public float timeCompleted = 0;
    bool MovingForward = true;
    bool IsRunning;
    public float SensorRange = 10;
    public List<float> fitness = new List<float>();
    int WallHits; 
    IBlackBox box;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
        // Five sensors: Front, left front, left, right front, right 

        if (IsRunning)
        {
            float frontSensor = 0;
            float leftFrontSensor = 0;
            float leftSensor = 0;
            float rightFrontSensor = 0;
            float rightSensor = 0;
            // Front sensor
            RaycastHit hit;
            if (Physics.Raycast(transform.position + transform.forward * 1.1f, transform.TransformDirection(new Vector3(0, 0, 1).normalized), out hit, SensorRange))
            {
                if (hit.collider.tag.Equals("Wall"))
                {
                    frontSensor = 1 - hit.distance / SensorRange;
                }
            }

            if (Physics.Raycast(transform.position + transform.forward * 1.1f, transform.TransformDirection(new Vector3(0.5f, 0, 1).normalized), out hit, SensorRange))
            {
                if (hit.collider.tag.Equals("Wall"))
                {
                    rightFrontSensor = 1 - hit.distance / SensorRange;
                }
            }

            if (Physics.Raycast(transform.position + transform.forward * 1.1f, transform.TransformDirection(new Vector3(1, 0, 0).normalized), out hit, SensorRange))
            {
                if (hit.collider.tag.Equals("Wall"))
                {
                    rightSensor = 1 - hit.distance / SensorRange;
                }
            }

            if (Physics.Raycast(transform.position + transform.forward * 1.1f, transform.TransformDirection(new Vector3(-0.5f, 0, 1).normalized), out hit, SensorRange))
            {
                if (hit.collider.tag.Equals("Wall"))
                {
                    leftFrontSensor = 1 - hit.distance / SensorRange;
                }
            }

            if (Physics.Raycast(transform.position + transform.forward * 1.1f, transform.TransformDirection(new Vector3(-1, 0, 0).normalized), out hit, SensorRange))
            {
                if (hit.collider.tag.Equals("Wall"))
                {
                    leftSensor = 1 - hit.distance / SensorRange;
                }
            }

            ISignalArray inputArr = box.InputSignalArray;
            inputArr[0] = frontSensor;
            inputArr[1] = leftFrontSensor;
            inputArr[2] = leftSensor;
            inputArr[3] = rightFrontSensor;
            inputArr[4] = rightSensor;

            box.Activate();

            ISignalArray outputArr = box.OutputSignalArray;

            var steer = (float)outputArr[0] * 2 - 1;
            var gas = (float)outputArr[1] * 2 - 1;

            var moveDist = gas * Speed * Time.deltaTime;
            var turnAngle = steer * TurnSpeed * Time.deltaTime * gas;

            transform.Rotate(new Vector3(0, turnAngle, 0));
            transform.Translate(Vector3.forward * moveDist);
        }
    }

    public override void Stop()
    {
        this.IsRunning = false;
    }

    public override void Activate(IBlackBox box)
    {
        this.box = box;
        this.IsRunning = true;
    }

    public void FinishedTrack()
    {    
        GameObject eval = GameObject.Find("TrackLoader");
        float timeLeft = eval.GetComponent<TrackLoader>().timeLeft;
        float totalTime = eval.GetComponent<TrackLoader>().trackTime;
        timeCompleted = totalTime / (totalTime - timeLeft);
    }

    public override float GetFitness() 
    {
        float total = 0.0f;

        foreach(float fit in fitness)
        {
            total += fit;
        }
        if (fitness.Count > 0) return total /= fitness.Count;
        else return 0f;

    }

    public void storeCurrentTrackFitness()
    {
        print("storing current fitness");
        int piece = CurrentPiece;

        progress = CurrentPiece / 14f;

        fitness.Add(timeCompleted + progress);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("Road"))
        {
            RoadPiece rp = collision.collider.GetComponent<RoadPiece>();
          //  print(collision.collider.tag + " " + rp.PieceNumber);
            
            if ((rp.PieceNumber != LastPiece) && (rp.PieceNumber == CurrentPiece + 1 || (MovingForward && rp.PieceNumber == 0)))
            {
                LastPiece = CurrentPiece;
                CurrentPiece = rp.PieceNumber;
                MovingForward = true;                
            }
            else
            {
                MovingForward = false;
            }
            if (rp.PieceNumber == 0)
            {
                CurrentPiece = 0;
            }
        }
        else if (collision.collider.tag.Equals("Wall"))
        {
            WallHits++;
        }
    }



    //void OnGUI()
    //{
    //    GUI.Button(new Rect(10, 200, 100, 100), "Forward: " + MovingForward + "\nPiece: " + CurrentPiece + "\nLast: " + LastPiece + "\nLap: " + Lap);
    //}
    
}
