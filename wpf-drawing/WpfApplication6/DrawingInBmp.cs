using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using WpfApplication6.winformCtrl;

namespace WpfApplication6
{
    /*
     * 本来是完成了将线先画在图片上，然后在窗口上画图片
     */
    class DrawingInBmp
    {
        public DrawingInBmp(MainWindow window)
        {
            m_MainWindow = window;
            AddPanelToWindow(window);
        }
        private MainWindow m_MainWindow;
        private FormDrawing m_DrawingPanel;

        private void AddPanelToWindow(MainWindow window)
        {
            WindowsFormsHost host = new WindowsFormsHost();
            //host.Background = Brushes.White;
            host.Width = window.Width - 100;
            host.Height = window.Height - 100;

            m_DrawingPanel = new FormDrawing();
            host.Child = m_DrawingPanel;
            window.grid1.Children.Add(host);

            PrePareDrawing((int)host.Width, (int)host.Height);
            
        }
        
        private Bitmap m_Bitmap;
        private Graphics m_Graphic;

        private void PrePareDrawing(int width, int height)
        {
            m_Bitmap = new Bitmap(width, height);
            m_Graphic = Graphics.FromImage(m_Bitmap);
            m_Graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

        }

        #region 绘画事件处理

        private Point _begin = new Point(0, 0);
        private Point _end;
        private bool bBegin = false;
        private List<MyLine> _Points = new List<MyLine>();
        private List<MyPloyLine> _Ploylines = new List<MyPloyLine>();
        private Pen _pen = new Pen(Brushes.Black);
        private MyPloyLine m_PolyLine = new MyPloyLine();
        public void BeginDrawing()
        {
            m_DrawingPanel.MouseDown += m_DrawingPanel_MouseDown;
            m_DrawingPanel.MouseMove += m_DrawingPanel_MouseMove;
            m_DrawingPanel.MouseUp += m_DrawingPanel_MouseUp;
            m_DrawingPanel.Paint += m_DrawingPanel_Paint;

            m_DrawingPanel.KeyDown += m_DrawingPanel_KeyDown;
        }



        public void EndDrawing()
        {
            m_DrawingPanel.MouseDown -= m_DrawingPanel_MouseDown;
            m_DrawingPanel.MouseMove -= m_DrawingPanel_MouseMove;
            m_DrawingPanel.MouseUp -= m_DrawingPanel_MouseUp;
            m_DrawingPanel.Paint -= m_DrawingPanel_Paint;

            m_DrawingPanel.KeyDown -= m_DrawingPanel_KeyDown;
        }
        void m_DrawingPanel_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            /*这里其实是可以接收到任何按键消息的*/
            if (e.KeyCode == Keys.Enter)
            {
                //m_PolyLine.Points.Add(new Point()
                _Ploylines.Add(m_PolyLine.Clone() as MyPloyLine);
                m_PolyLine.Points.Clear();
                Invalidate(false);
                bBegin = false;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                m_PolyLine.Points.Clear();
                Invalidate(false);
                bBegin = false;
            }
            else if (e.KeyCode == Keys.Delete)
            {
                MessageBox.Show("del key is down");
            }
        }

        void m_DrawingPanel_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            _end = e.Location;
            bBegin = false;
            if (m_Graphic != null)
            {
                m_Graphic.DrawLine(_pen, _begin, _end);
            }
        }

        void m_DrawingPanel_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (bBegin)
            {
                _end = e.Location;
                if (m_Graphic != null)
                {
                    m_Graphic.Clear(Color.Transparent);
                    m_Graphic.DrawLine(_pen, _begin, _end);
                }
                Invalidate(false);
            }
        }

        void m_DrawingPanel_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            bBegin = true;
            _begin = e.Location;
            _end = _begin;
            if (m_Graphic != null)
                m_Graphic.DrawLine(_pen, _begin, _end);
            Invalidate(false);
        }

        private void Invalidate(bool bRefresh)
        {
            m_DrawingPanel.Invalidate(bRefresh);
        }

        void m_DrawingPanel_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.DrawImage(m_Bitmap, 0, 0);
        }

        public class MyLine
        {
            public MyLine(Point begin, Point end)
            {
                pBegin = begin;
                pEnd = end;
            }

            public Point pBegin { get; set; }
            public Point pEnd { get; set; }
        }

        public class MyPloyLine : ICloneable
        {
            public MyPloyLine()
            {
                _Points = new List<Point>();
            }

            private List<Point> _Points;

            public List<Point> Points
            {
                get { return _Points; }
                set { _Points = value; }
            }

            public object Clone()
            {
                MyPloyLine ployLine = new MyPloyLine();
                foreach (Point item in this.Points)
                {
                    ployLine.Points.Add(item);
                }
                return ployLine;
            }
        }

        #endregion
    }


}
