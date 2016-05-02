using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace MovementDetectionLibrary
{
    public enum BodyParts
    {
        HipCenter,
        Spine,
        ShoulderCenter,
        Head,
        ShoulderLeft,
        ElbowLeft,
        WristLeft,
        HandLeft,
        ShoulderRight,
        ElbowRight,
        WristRight,
        HandRight,
        HipLeft,
        KneeLeft,
        AnkleLeft,
        FootLeft,
        HipRight,
        KneeRight,
        AnkleRight,
        FootRight
    };

    public struct BodyPointPosition
    {
        public BodyParts name;
        public float x;
        public float y;
        public float z;
    }
    

    class BodyPoint
    {

        private BodyPointPosition currentPosition;
        public BodyParts pointRepresented;

        Dictionary<BodyParts, JointType> bodyParts;
        public void FillDictionary()
        {
            bodyParts = new Dictionary<BodyParts, JointType>();
            bodyParts.Add(BodyParts.HipCenter, JointType.HipCenter);
            bodyParts.Add(BodyParts.Spine, JointType.Spine);
            bodyParts.Add(BodyParts.ShoulderCenter, JointType.ShoulderCenter);
            bodyParts.Add(BodyParts.Head, JointType.Head);
            bodyParts.Add(BodyParts.ShoulderLeft, JointType.ShoulderLeft);
            bodyParts.Add(BodyParts.ElbowLeft, JointType.ElbowLeft);
            bodyParts.Add(BodyParts.WristLeft, JointType.WristLeft);
            bodyParts.Add(BodyParts.HandLeft, JointType.HandLeft);
            bodyParts.Add(BodyParts.ShoulderRight, JointType.ShoulderRight);
            bodyParts.Add(BodyParts.ElbowRight, JointType.ElbowRight);
            bodyParts.Add(BodyParts.WristRight, JointType.WristRight);
            bodyParts.Add(BodyParts.HandRight, JointType.HandRight);
            bodyParts.Add(BodyParts.HipLeft, JointType.HipLeft);
            bodyParts.Add(BodyParts.KneeLeft, JointType.KneeLeft);
            bodyParts.Add(BodyParts.AnkleLeft, JointType.AnkleLeft);
            bodyParts.Add(BodyParts.FootLeft, JointType.FootLeft);
            bodyParts.Add(BodyParts.HipRight, JointType.HipRight);
            bodyParts.Add(BodyParts.KneeRight, JointType.KneeRight);
            bodyParts.Add(BodyParts.AnkleRight, JointType.AnkleRight);
            bodyParts.Add(BodyParts.FootRight, JointType.FootRight);
        }

        public BodyPoint(BodyParts point)
        {
            FillDictionary();
            pointRepresented = point;
            Console.WriteLine("Inicia" + point.ToString());
            currentPosition.name = point;
            currentPosition.x = 0.0f;
            currentPosition.y = 0.0f;
            currentPosition.z = 0.0f;
        }
        
        public BodyPointPosition getCurrentPosition()
        {
            return currentPosition;
        }

        public BodyPointPosition updatePosition(Skeleton currentBody)
        {
            Joint jointToShow = currentBody.Joints[bodyParts[pointRepresented]];
            SkeletonPoint positionToShow = jointToShow.Position;
            currentPosition.x = positionToShow.X;
            currentPosition.y = positionToShow.Y;
            currentPosition.z = positionToShow.Z;
            return currentPosition;
        }
    }
}
