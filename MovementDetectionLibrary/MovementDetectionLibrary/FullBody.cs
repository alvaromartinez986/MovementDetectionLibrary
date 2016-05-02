using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace MovementDetectionLibrary
{
    public class FullBody
    {
        private Dictionary<BodyParts, BodyPoint> pointsBodyCollection;
        private KinectSensor sensorConnection;
        private Skeleton currentBody;
        public BodyParts[] namePointsCollection;

        public event BodyChangedHandler BodyChanged;
        public delegate void BodyChangedHandler(FullBody f, List<BodyPointPosition> e);

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
            pointsBodyCollection = new Dictionary<BodyParts, BodyPoint>();
            namePointsCollection = new BodyParts [] {
                BodyParts.HipCenter,
                BodyParts.Spine,
                BodyParts.ShoulderCenter,
                BodyParts.Head,
                BodyParts.ShoulderLeft,
                BodyParts.ElbowLeft,
                BodyParts.WristLeft,
                BodyParts.HandLeft,
                BodyParts.ShoulderRight,
                BodyParts.ElbowRight,
                BodyParts.WristRight,
                BodyParts.HandRight,
                BodyParts.HipLeft,
                BodyParts.KneeLeft,
                BodyParts.AnkleLeft,
                BodyParts.FootLeft,
                BodyParts.HipRight,
                BodyParts.KneeRight,
                BodyParts.AnkleRight,
                BodyParts.FootRight};
            //connectSensor();
            initBodyPoints();
            currentBody = new Skeleton();
            connectSensor();
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
                    //Console.WriteLine("Nuevo frame");
                    if (body.TrackingState == SkeletonTrackingState.Tracked)
                    {
                        Console.WriteLine("Nuevo tracked frame");
                        currentBody = body;
                        updateBodyPoints(body);
                        //Console.WriteLine("Skeleton track");
                        verifyBodyPosition(body);
                    }
                }

            }
            


        }


        private void initBodyPoints()
        {
            foreach (BodyParts namePoint in namePointsCollection)
            {
                Console.WriteLine("Entra 2");
                BodyPoint jointPoint = new BodyPoint(namePoint);

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


        private void updateBodyPoints(Skeleton body)
        {
            //BodyParts[] pointsDifferent = { };
            List<BodyPointPosition> pointsDifferent = new List<BodyPointPosition>();
            foreach (BodyParts namePoint in namePointsCollection)
            {
                try
                {
                    BodyPointPosition last = pointsBodyCollection[namePoint].getCurrentPosition();
                    Console.WriteLine("Llega 3");
                    BodyPointPosition current = pointsBodyCollection[namePoint].updatePosition(currentBody);
                    if (last.Equals(current)){
                        Console.WriteLine("Son iguales");
                        //Console.WriteLine(pointsBodyCollection[namePoint].pointRepresented.ToString());
                    } else{
                        pointsDifferent.Add(last);
                        Console.WriteLine("Son distintos");
                        //Console.WriteLine(pointsBodyCollection[namePoint].pointRepresented.ToString());

                        
                    }
                    //Console.WriteLine("Actualizo posiciones");
                }
                catch
                {
                    Console.WriteLine("Exception en updateBodyPoints");
                }

            }

            
            BodyChanged(this, pointsDifferent);

        }


        public BodyPointPosition returnPosition(BodyParts nameBodyPoint)
        {
            try
            {
                BodyPoint pointTrack = pointsBodyCollection[nameBodyPoint];
                //Console.WriteLine("Llega 1");
                return pointTrack.getCurrentPosition();

            }
            catch
            {
                Console.WriteLine("Exception");

                return new BodyPointPosition();
            }
        }


        /**
         *
         */
        public int verifyBodyPosition(Skeleton body)
        {
            

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
