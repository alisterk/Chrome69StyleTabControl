using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace WindowsFormsApp1
{
    public class Chrome69Tabcontrol : TabControl
    {
        const int CLOSE_SIZE = 16;

        public Chrome69Tabcontrol()
            :base()
        {
            SetStyles();
            this.SizeMode = TabSizeMode.Fixed;
            this.Dock = DockStyle.Fill;
            this.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.ItemSize = new System.Drawing.Size(245, 35);
        }

        private void SetStyles()
        {
            base.SetStyle(
                ControlStyles.DoubleBuffer |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor, true);
            base.UpdateStyles();
        }

        public override Rectangle DisplayRectangle
        {
            get
            {
                Rectangle rect = base.DisplayRectangle;
                return new Rectangle(rect.Left - 8, rect.Top-1, rect.Width + 12, rect.Height + 8);// 解决系统TabControl多余边距问题 
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Refresh();
            this.Update();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Rectangle rect = this.ClientRectangle; //获取tabcontrol背景区域
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;//画图质量
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
            using (BufferedGraphics bufferedGraphics = BufferedGraphicsManager.Current.Allocate(e.Graphics, rect))//创建缓冲 Graphics对象，区域
            {
                bufferedGraphics.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(230, 232, 236)), rect);//填充背景
                for (int index = 0; index < this.TabCount; index++)
                {
                    DrawTabPage(bufferedGraphics.Graphics, this.GetTabRect(index),index);
                }
                bufferedGraphics.Render(e.Graphics); //获取缓冲 Graphics对象，区域
            }
        }

        private void DrawTabPage(Graphics graphics, Rectangle rectangle,int index)
        {
            graphics.SmoothingMode = SmoothingMode.HighQuality;//画图质量
            graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            StringFormat sf = new StringFormat();
            sf.Trimming = StringTrimming.EllipsisCharacter;
            sf.FormatFlags = StringFormatFlags.NoWrap;
            Rectangle fontRect = new Rectangle(rectangle.X +40, rectangle.Y + 7, rectangle.Width, this.TabPages[index].Font.Height);//文字区域
            Rectangle rectClose = GetCloseRect(rectangle);
            Point p5 = new Point(rectangle.Left, 7);//选项卡添加按钮位置，Lift调整变化为左右，负的约多越向左，y为上下
            Point p6 = new Point(rectClose.X-12, 12);//选项卡关闭按钮位置,x调整变化为左右，负的约多越向左，y为上下
            try
            {
                if (index == this.TabCount - 1)
                {
                    using (Bitmap Add = Properties.Resources.Add)
                    {
                        graphics.DrawImage(Add, p5);
                    }
                }
                else
                {
                    if (index==this.SelectedIndex)
                    {
                        graphics.FillPath(new SolidBrush(Color.FromArgb(255, 255, 255)), CreateTabPath(rectangle)); //填充当前选项卡背景
                        graphics.DrawString(this.TabPages[index].Text, this.TabPages[index].Font, new SolidBrush(Color.SlateGray), fontRect, sf); //当前选项卡文字绘制
                        if (this.ImageList != null)
                        {
                            int imgindex = this.TabPages[index].ImageIndex;
                            string key = this.TabPages[index].ImageKey;
                            Image icon = new Bitmap(32, 32);
                            if (imgindex > -1)
                            {
                                icon = this.ImageList.Images[imgindex];
                            }
                            if (!string.IsNullOrEmpty(key))
                            {
                                icon = this.ImageList.Images[key];
                            }
                            graphics.DrawImage(icon, rectangle.Left + 22, rectangle.Top + 9);
                        }
                        using (Bitmap Close = Properties.Resources.Close)
                        {
                            graphics.DrawImage(Close, p6);
                        }
                    }
                    else
                    {
                        graphics.FillPath(new SolidBrush(Color.FromArgb(230, 232, 236)), CreateTabPath(rectangle)); //填充非选中选项卡背景
                        graphics.DrawString(this.TabPages[index].Text, this.TabPages[index].Font, new SolidBrush(Color.Gray), fontRect, sf); //非选中选项卡文字绘制 
                        if (this.ImageList != null)
                        {
                            int imgindex = this.TabPages[index].ImageIndex;
                            string key = this.TabPages[index].ImageKey;
                            Image icon = new Bitmap(32, 32);
                            if (imgindex > -1)
                            {
                                icon = this.ImageList.Images[imgindex];
                            }
                            if (!string.IsNullOrEmpty(key))
                            {
                                icon = this.ImageList.Images[key];
                            }
                            graphics.DrawImage(icon, rectangle.Left + 22, rectangle.Top + 9);
                        }
                        using (Bitmap Close = Properties.Resources.Close)
                        {
                            graphics.DrawImage(Close, p6);
                        }
                    }
                    }
            }
            catch (System.NullReferenceException)
            {
            }
        }

        protected override void OnSelecting(TabControlCancelEventArgs e)
        {
            if (e.TabPageIndex == this.TabPages.Count - 1)//拦截添加选项卡事件
                e.Cancel = true;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            int lastIndex = this.TabCount - 1;
            Bitmap AddImage = Properties.Resources.Add;
            Point p5 = new Point(this.GetTabRect(lastIndex).Left, 5);//选项卡添加按钮位置，Lift调整变化为左右，负的约多越向左，y为上下
            var AddImgRec = new Rectangle(p5, AddImage.Size);
            if (this.GetTabRect(lastIndex).Contains(e.Location))//是否在选项卡内
            {
                if (AddImgRec.Contains(e.Location))//是否在选项卡添加图标内
                {
                    this.TabPages.Insert(lastIndex,"Chrome Tab");//切记添加的tabpage索引必须为TabCount-1，不然有bug
                    this.SelectedIndex = lastIndex;
                }
            }
            if (e.Button == MouseButtons.Left)
            {
                int x = e.X, y = e.Y;  
                Rectangle myTabRect = this.GetTabRect(this.SelectedIndex);
                myTabRect.Offset(myTabRect.Width - (CLOSE_SIZE + 23), 5);//计算两个按钮关闭区域,close-size+得越多区域越向左，height越大区域向下
                myTabRect.Width = CLOSE_SIZE;
                myTabRect.Height = CLOSE_SIZE;
                bool isClose = x > myTabRect.X && x < myTabRect.Right && y > myTabRect.Y && y < myTabRect.Bottom;//如果鼠标在关闭按钮区域内就关闭选项卡
                if (isClose == true)//判断鼠标是否在关闭按钮区域内
                {
                    if (this.TabPages.Count > 2)
                    {
                        TabPage tab = this.SelectedTab;//如果标签页大于2，移除当前选项卡，并释放资源
                        this.TabPages.Remove(tab);
                        this.SelectedTab.Refresh();
                        this.SelectedIndex = this.TabPages.Count - 2;
                        tab.Dispose();
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                    }
                    else
                    {
                        System.Environment.Exit(0);//如果标签页等于2，退出当前应用程序
                        Dispose();
                    }
                }
            }
        }

        private GraphicsPath CreateTabPath(Rectangle tabBounds)
        {
            GraphicsPath path = new GraphicsPath();
            int spread, eigth,sixth,quarter;
                spread = (int)Math.Floor((decimal)tabBounds.Height);
                eigth = (int)Math.Floor((decimal)tabBounds.Height * 1 / 11);
                sixth = (int)Math.Floor((decimal)tabBounds.Height * 3 / 10);
                quarter = (int)Math.Floor((decimal)tabBounds.Height * 2 / 3);
            path.AddCurve(new Point[] {  new Point(tabBounds.X+2, tabBounds.Bottom+2)
                                          ,new Point(tabBounds.X + sixth, tabBounds.Bottom - eigth)
                                          ,new Point(tabBounds.X + spread - quarter, tabBounds.Y + eigth)
                                          ,new Point(tabBounds.X + spread, tabBounds.Y)});
            path.AddLine(tabBounds.X + spread, tabBounds.Y, tabBounds.Right - spread, tabBounds.Y);
            path.AddCurve(new Point[] {  new Point(tabBounds.Right - spread, tabBounds.Y)
                                          ,new Point(tabBounds.Right - spread + quarter, tabBounds.Y + eigth)
                                          ,new Point(tabBounds.Right - sixth, tabBounds.Bottom - eigth)
                                          ,new Point(tabBounds.Right+2, tabBounds.Bottom+2)});

            path.CloseFigure();
            return path;
            
        }

        Rectangle GetCloseRect(Rectangle myTabRect)
        {
            myTabRect.Offset(myTabRect.Width - (CLOSE_SIZE + 10), 5);//关闭按钮区域
            myTabRect.Width = CLOSE_SIZE;
            myTabRect.Height = CLOSE_SIZE;
            return myTabRect;
        }
    }
}
