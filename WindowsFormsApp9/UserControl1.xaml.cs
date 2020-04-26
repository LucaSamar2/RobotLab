using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WindowsFormsApp9
{
    /// <summary>
    /// Logica di interazione per UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl 
    {
        //provides functionality to 3d models
        Model3DGroup RA = new Model3DGroup();
        Model3D basamento = null;
        Model3D link1 = null;
        Model3D link2 = null;
        Model3D link3 = null;
        Model3D link4 = null;
        Model3D link5 = null;
        Model3D link6 = null;
        Model3D leva = null;
        Model3D asta = null;
        //provides render to model3d objects
        ModelVisual3D RoboticArm = new ModelVisual3D();

        //directroy of all stl files
        private const string MODEL_PATH1 = "Base.stl";
        private const string MODEL_PATH2 = "Asse1.stl";
        private const string MODEL_PATH3 = "Asse2.stl";
        private const string MODEL_PATH4 = "Asse3.stl";
        private const string MODEL_PATH5 = "Asse4.stl";
        private const string MODEL_PATH6 = "Asse5.stl";
        private const string MODEL_PATH7 = "Asse6.stl";
        private const string MODEL_PATH8 = "Leva.stl";
        private const string MODEL_PATH9 = "Asta.stl";

        RotateTransform3D R = new RotateTransform3D();
        TranslateTransform3D T = new TranslateTransform3D();

        public UserControl1()
        {
            InitializeComponent();
            RoboticArm.Content = Initialize_Environment(MODEL_PATH1, MODEL_PATH2, MODEL_PATH3, MODEL_PATH4, MODEL_PATH5, MODEL_PATH6,MODEL_PATH7, MODEL_PATH8, MODEL_PATH9);
            viewPort3d.Children.Add(RoboticArm);
        }

        private Model3DGroup Initialize_Environment(string model1, string model2, string model3, string model4, string model5 , string model6, string model7, string model8, string model9)
        {

            try
            {


                viewPort3d.RotateGesture = new MouseGesture(MouseAction.LeftClick);
                ModelImporter importa = new ModelImporter();

                basamento = importa.Load(model1);
                link1 = importa.Load(model2);
                link2 = importa.Load(model3);
                link3 = importa.Load(model4);
                link4 = importa.Load(model5);
                link5 = importa.Load(model6);
                link6 = importa.Load(model7);
                leva = importa.Load(model8);
                asta = importa.Load(model9);

                Transform3DGroup F1 = new Transform3DGroup();
                Transform3DGroup F2 = new Transform3DGroup();
                Transform3DGroup F3 = new Transform3DGroup();
                Transform3DGroup F4 = new Transform3DGroup();
                Transform3DGroup F5 = new Transform3DGroup();
                Transform3DGroup F6 = new Transform3DGroup();
                Transform3DGroup F7 = new Transform3DGroup();
                Transform3DGroup F8 = new Transform3DGroup();
                Transform3DGroup F9 = new Transform3DGroup();

                T = new TranslateTransform3D(0, 0, 2.55);
                F1.Children.Add(T);

                T = new TranslateTransform3D(0, 0, 0);
                R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 0, 1),0), new Point3D(0, 0, 0));
                F2.Children.Add(T);
                F2.Children.Add(R);
                F2.Children.Add(F1);

                T = new TranslateTransform3D(4.4055, -1.35, 5.4723);
                R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), 0), new Point3D(4.4055, -1.35, 5.4723));
                F3.Children.Add(T);
                F3.Children.Add(R);
                F3.Children.Add(F2);

                T = new TranslateTransform3D(0, -0.55, 11.7);
                R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), 0), new Point3D(0, -0.55, 11.7));
                F4.Children.Add(T);
                F4.Children.Add(R);
                F4.Children.Add(F3);

                T = new TranslateTransform3D(1.3, 1.85, 1.8);
                R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), 0), new Point3D(1.3, 1.85, 1.8));
                F5.Children.Add(T);
                F5.Children.Add(R);
                F5.Children.Add(F4);

                T = new TranslateTransform3D(10.5, 1.4, 0);
                R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), 0), new Point3D(10.5, 1.4, 0));
                F6.Children.Add(T);
                F6.Children.Add(R);
                F6.Children.Add(F5);

                T = new TranslateTransform3D(1.7, -1.45, 0);
                R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), 0), new Point3D(1.7, 1.45, 0));
                F7.Children.Add(T);
                F7.Children.Add(R);
                F7.Children.Add(F6);

                T = new TranslateTransform3D(4.4055, 2.1, 5.4723);
                R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, -1, 0), 0), new Point3D(4.4055, 2.1, 5.4723));
                F8.Children.Add(T);
                F8.Children.Add(R);
                F8.Children.Add(F2);

                T = new TranslateTransform3D(3.8, 0, 0);
                R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), 0), new Point3D(3.8, 0, 0));
                F9.Children.Add(T);
                F9.Children.Add(R);
                F9.Children.Add(F8);


                basamento.Transform = F1;
                link1.Transform = F2;
                link2.Transform = F3;
                link3.Transform = F4;
                link4.Transform = F5;
                link5.Transform = F6;
                link6.Transform = F7;
                leva.Transform = F8;
                asta.Transform = F9;

                RA.Children.Add(basamento);
                RA.Children.Add(link1);
                RA.Children.Add(link2);
                RA.Children.Add(link3);
                RA.Children.Add(link4);
                RA.Children.Add(link5);
                RA.Children.Add(link6);
                RA.Children.Add(asta);
                RA.Children.Add(leva);

            }
            catch (Exception e)
            {
                MessageBox.Show("Exception Error:" + e.StackTrace);
            }
            return RA;
        }


        public void refresh()
        {
            Transform3DGroup F1 = new Transform3DGroup();
            Transform3DGroup F2 = new Transform3DGroup();
            Transform3DGroup F3 = new Transform3DGroup();
            Transform3DGroup F4 = new Transform3DGroup();
            Transform3DGroup F5 = new Transform3DGroup();
            Transform3DGroup F6 = new Transform3DGroup();
            Transform3DGroup F7 = new Transform3DGroup();
            Transform3DGroup F8 = new Transform3DGroup();
            Transform3DGroup F9 = new Transform3DGroup();

            T = new TranslateTransform3D(0, 0, 2.55);
            F1.Children.Add(T);

            T = new TranslateTransform3D(0, 0, 0);
            R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 0, 1), Robot.g1), new Point3D(0, 0, 0));
            F2.Children.Add(T);
            F2.Children.Add(R);
            F2.Children.Add(F1);

            T = new TranslateTransform3D(4.4055, -1.35, 5.4723);
            R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, -1, 0), Robot.g2-90), new Point3D(4.4055, -1.35, 5.4723));
            F3.Children.Add(T);
            F3.Children.Add(R);
            F3.Children.Add(F2);

            T = new TranslateTransform3D(0,-0.55, 11.7);
            R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), -Robot.g3+90), new Point3D(0, -0.55, 11.7));
            F4.Children.Add(T);
            F4.Children.Add(R);
            F4.Children.Add(F3);

            T = new TranslateTransform3D(1.3, 1.85, 1.8);
            R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), Robot.g4), new Point3D(1.3, 1.85, 1.8));
            F5.Children.Add(T);
            F5.Children.Add(R);
            F5.Children.Add(F4);

            T = new TranslateTransform3D(10.5, 1.4, 0);
            R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), -Robot.g5), new Point3D(10.5, 1.4, 0));
            F6.Children.Add(T);
            F6.Children.Add(R);
            F6.Children.Add(F5);

            T = new TranslateTransform3D(1.7, -1.45, 0);
            R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), Robot.g6), new Point3D(1.7, -1.45, 0));
            F7.Children.Add(T);
            F7.Children.Add(R);
            F7.Children.Add(F6);

            T = new TranslateTransform3D(4.4055, 2.1, 5.4723);
            R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), Robot.ga1), new Point3D(4.4055, 2.1, 5.4723));
            F8.Children.Add(T);
            F8.Children.Add(R);
            F8.Children.Add(F2);

            T = new TranslateTransform3D(3.8,0,0);
            R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), Robot.ga2), new Point3D(3.8, 0, 0));
            F9.Children.Add(T);
            F9.Children.Add(R);
            F9.Children.Add(F8);

            basamento.Transform = F1;
            link1.Transform = F2;
            link2.Transform = F3;
            link3.Transform = F4;
            link4.Transform = F5;
            link5.Transform = F6;
            link6.Transform = F7;
            leva.Transform = F8;
            asta.Transform = F9;

        }
    }
}

