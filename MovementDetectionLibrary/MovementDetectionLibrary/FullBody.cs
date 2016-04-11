using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace MovementDetectionLibrary
{
    class FullBody
    {
        private Dictionary<string, BodyPoint> pointsBodyCollection;
        private KinectSensor sensorConnection;

        public FullBody()
        {
            sensorConnection = KinectSensor.KinectSensors.FirstOrDefault();
            try
            {
                sensorConnection.SkeletonStream.Enable();
                sensorConnection.Start();

            }
            catch
            {
                Console.WriteLine("Error, connection failed");
            }
            pointsBodyCollection = new Dictionary<string, BodyPoint>();
            //connectSensor();

        }

        /**
         * Function to initalizate the connection with the sensor, in this case with the Kinect
         */
        private void connectSensor()
        {            

            sensorConnection.SkeletonFrameReady += sensorConnection_SkeletonFrameReady;

        }


        private void sensorConnection_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {

            Skeleton[] skeletonCollection = null;

            using(SkeletonFrame framesSke = e.OpenSkeletonFrame())
            {
                if (framesSke!= null)
                {
                    skeletonCollection = new Skeleton[framesSke.SkeletonArrayLength];
                    framesSke.CopySkeletonDataTo(skeletonCollection);
                }
            }

            if (skeletonCollection == null) return ;
            else
            {
                foreach (Skeleton body in skeletonCollection)
                {
                    if (body.TrackingState == SkeletonTrackingState.Tracked)
                    {
                        //Console.WriteLine("Skeleton track");
                        verifyBodyPosition(body);
                    }
                }

            }
            


        }


        private void initBodyPoints(Skeleton body)
        {
            
            String[] namePointsCollection = {"Head", "AnkleLeft", "Left ankle", "AnkleRight", "ElbowLeft", "ElbowRight", "FootLeft",
                                    "FootRight", "HandLeft", "HandRight", "HandTipLeft", "HandTipRight", "Head", "HipLeft", "HipRight", "KneeLeft",
                                        "KneeRight", "Neck", "ShoulderLeft", "ShoulderRight", "SpineBase", "SpineMid", "SpineShoulder",
                                            "ThumbLeft", "ThumbRight", "WristLeft", "WristRight"};

            foreach (String namePoint in namePointsCollection)
            {
                BodyPoint jointPoint = new BodyPoint(namePoint, sensorConnection, body);

                try
                {
                    pointsBodyCollection.Add(namePoint, jointPoint);
                    //Console.WriteLine();
                }
                catch
                {
                    
                }

            }
            
        }


        public float returnPosition(string nameBodyPoint)
        {
            connectSensor();
            try
            {
                BodyPoint pointTrack = pointsBodyCollection[nameBodyPoint];

                return pointTrack.returnPosition();

            }
            catch
            {
                Console.WriteLine();

                return -1;
            }
        }


        /**
         *
         */
        public int verifyBodyPosition(Skeleton body)
        {
            initBodyPoints(body);

            if (body.ClippedEdges == 0)
            {
                return 1;
            }
            else
            {
                if ((body.ClippedEdges & FrameEdges.Bottom) != 0)
                    return 2;
                if ((body.ClippedEdges & FrameEdges.Top) != 0)
                    return 3;
                if ((body.ClippedEdges & FrameEdges.Right) != 0)
                    return 4;
                if ((body.ClippedEdges & FrameEdges.Left) != 0)
                    return 5;
            }

            return 6;

        }

        
    }
}
