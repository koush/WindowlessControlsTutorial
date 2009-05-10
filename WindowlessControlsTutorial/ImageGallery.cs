using System;
using WindowlessControls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WindowlessControls.CommonControls;
using System.Reflection;
using System.IO;

namespace WindowlessControlsTutorial
{
    public partial class ImageGallery : Form
    {
        // an ImageResourcePresenter will be shown as a WindowlessImage
        // with a WindowlessBackground that is visible when it is selected
        class ImageResourcePresenter : OverlayPanel, IInteractiveContentPresenter
        {
            WindowlessRectangle myRectangle = new WindowlessRectangle();
            WindowlessImage myImage = new WindowlessImage();

            public ImageResourcePresenter()
            {
                // spacing between images
                Margin = new Thickness(5, 5, 5, 5);
                // limit the maximum size of an item
                MaxWidth = 100;
                MaxHeight = 100;
                // stretch to fit
                HorizontalAlignment = WindowlessControls.HorizontalAlignment.Stretch;
                VerticalAlignment = VerticalAlignment.Stretch;

                // allow a 5 pixel border between the image and the rectangular selected background
                myImage.Margin = new Thickness(5, 5, 5, 5);
                myImage.VerticalAlignment = VerticalAlignment.Center;
                myImage.HorizontalAlignment = WindowlessControls.HorizontalAlignment.Center;

                // set up the rectangle color and make it fill the region
                myRectangle.Color = SystemColors.Highlight;
                myRectangle.HorizontalAlignment = WindowlessControls.HorizontalAlignment.Stretch;
                myRectangle.VerticalAlignment = VerticalAlignment.Stretch;
                // the rectangle does not paint by default
                myRectangle.PaintSelf = false;

                Controls.Add(myRectangle);
                Controls.Add(myImage);
            }

            #region IInteractiveStyleControl Members

            public void ApplyFocusedStyle()
            {
                // make the rectangle paint to denote that it is focused
                myRectangle.PaintSelf = true;
            }

            public void ApplyClickedStyle()
            {
                // when clicked, remove the margin, so the image can "grow"
                myImage.Margin = Thickness.Empty;
            }

            #endregion

            #region IContentPresenter Members

            string myName;
            public object Content
            {
                get
                {
                    return myName;
                }
                set
                {
                    myName = value as string;
                    // see if this image is a file or a resource, and load it
                    if (!File.Exists(myName))
                    {
                        myImage.Bitmap = PlatformBitmap.FromResource(myName);
                    }
                    else
                    {
                        myImage.Bitmap = PlatformBitmap.From(myName);
                    }
                }
            }

            #endregion
        }

        // these are image extensions
        static string[] imageExtensions = new string[] { "jpg", "bmp", "png", "gif" };
        // An ItemsControl is a data bound control
        ItemsControl myItemsControl = new ItemsControl();

        public ImageGallery()
        {
            InitializeComponent();

            VerticalStackPanelHost scrollHost = new VerticalStackPanelHost();
            StackPanel stack = scrollHost.Control;
            stack.HorizontalAlignment = WindowlessControls.HorizontalAlignment.Stretch;
            myHost.Control.Controls.Add(scrollHost);

            myHost.AutoScroll = true;

            // we need to layer two controls on top of another: a background, with another panel containing more controls
            OverlayPanel overlay = new OverlayPanel();
            stack.Controls.Add(overlay);

            // set up the background bitmap and control
            PlatformBitmap backgroundBitmap = PlatformBitmap.FromResource("Wallpaper.jpg");
            WindowlessImage background = new WindowlessImage(backgroundBitmap);
            background.Stretch = Stretch.Uniform;
            overlay.Controls.Add(background);

            // set up the foreground
            StackPanel foreground = new StackPanel();
            overlay.Controls.Add(foreground);

            // set up the ItemsControl which will hold the images
            myItemsControl.BackColor = Color.Transparent;
            // the images will be contained in a wrap panel
            myItemsControl.Control = new WrapPanel();
            // this tells the ItemsControl what is used to represent the content items
            myItemsControl.ContentPresenter = typeof(ImageResourcePresenter);
            foreground.Controls.Add(myItemsControl);

            // let's get a list of image resources
            Assembly ass = Assembly.GetCallingAssembly();
            string[] names = ass.GetManifestResourceNames();
            foreach (string name in names)
            {
                foreach (string extension in imageExtensions)
                {
                    if (name.EndsWith(extension, StringComparison.CurrentCultureIgnoreCase))
                    {
                        myItemsControl.Items.Add(name);
                        break;
                    }
                }
            }

            // now let's search the file system for images
            foreach (string name in Directory.GetFiles("\\My Documents\\My Pictures"))
            {
                foreach (string extension in imageExtensions)
                {
                    if (name.EndsWith(extension, StringComparison.CurrentCultureIgnoreCase))
                    {
                        myItemsControl.Items.Add(name);
                        break;
                    }
                }
            }
        }

        private void myCloseMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}