using System;
using WindowlessControls;
using WindowlessControls.CommonControls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.WindowsMobile.PocketOutlook;

namespace WindowlessControlsTutorial
{
    public class ContactPresenter : StackPanel, IInteractiveContentPresenter
    {
        // selection state rectangle
        WindowlessRectangle myRectangle = new WindowlessRectangle();
        // contact picture
        WindowlessImage myImage = new WindowlessImage();

        // obvious properties
        WindowlessLabel myName = new WindowlessLabel(string.Empty, WindowlessLabel.DefaultBoldFont);
        WindowlessLabel myNumber = new WindowlessLabel();
        WindowlessLabel myAddress = new WindowlessLabel();
        WindowlessLabel myEmail = new WindowlessLabel();
        // this panel will house the email and address, and only be visible when focused
        StackPanel myExtendedInfo = new StackPanel();

        public ContactPresenter()
        {
            OverlayPanel overlay = new OverlayPanel();

            // stretch to fit
            overlay.HorizontalAlignment = WindowlessControls.HorizontalAlignment.Stretch;
            overlay.VerticalAlignment = VerticalAlignment.Stretch;

            // dock the picture to the right and the info to the left
            DockPanel dock = new DockPanel();
            StackPanel left = new StackPanel();
            left.Layout = new DockLayout(new LayoutMeasurement(0, LayoutUnit.Star), DockStyle.Left);
            myImage.Layout = new DockLayout(new LayoutMeasurement(0, LayoutUnit.Star), DockStyle.Right);
            myImage.MaxHeight = 100;
            myImage.MaxWidth = 100;

            dock.Controls.Add(myImage);
            dock.Controls.Add(left);
            // 5 pixel border around the item contents
            dock.Margin = new Thickness(5, 5, 5, 5);

            // make the overlay fit the dock, so as to limit the size of the selection rectangle
            overlay.FitWidthControl = overlay.FitHeightControl = dock;

            // set up the rectangle color and make it fill the region
            myRectangle.Color = SystemColors.Highlight;
            myRectangle.HorizontalAlignment = WindowlessControls.HorizontalAlignment.Stretch;
            myRectangle.VerticalAlignment = VerticalAlignment.Stretch;
            // the rectangle does not paint by default
            myRectangle.PaintSelf = false;

            // add the extended info that is only visible when focused
            StackPanel nameAndNumber = new StackPanel();
            nameAndNumber.Controls.Add(myName);
            nameAndNumber.Controls.Add(myNumber);
            myExtendedInfo.Visible = false;
            myExtendedInfo.Controls.Add(myEmail);
            myExtendedInfo.Controls.Add(myAddress);

            // set up the left side
            left.Controls.Add(nameAndNumber);
            left.Controls.Add(myExtendedInfo);

            // add the foreground and the background selection
            overlay.Controls.Add(myRectangle);
            overlay.Controls.Add(dock);
            
            // add the item
            Controls.Add(overlay);

            // this is the bottom border
            WindowlessRectangle bottomBorder = new WindowlessRectangle(Int32.MaxValue, 1, Color.Gray);
            Controls.Add(bottomBorder);
        }

        #region IInteractiveStyleControl Members

        public void ApplyFocusedStyle()
        {
            // make the rectangle paint to denote that it is focused
            myRectangle.PaintSelf = true;
            myExtendedInfo.Visible = true;
        }

        public void ApplyClickedStyle()
        {
        }

        #endregion

        #region IContentPresenter Members

        Contact myContact;
        public object Content
        {
            get
            {
                return myContact;
            }
            set
            {
                // populate the various UI elements with the contact
                myContact = value as Contact;
                if (myContact.Picture != null)
                    myImage.Bitmap = new StandardBitmap(new Bitmap(myContact.Picture));

                myName.Text = myContact.FileAs;
                if (!string.IsNullOrEmpty(myContact.MobileTelephoneNumber))
                    myNumber.Text = myContact.MobileTelephoneNumber;
                else
                    myNumber.Text = myContact.HomeTelephoneNumber;

                myAddress.Text = string.Format("{0}\n{1} {2} {3}", myContact.HomeAddressStreet, myContact.HomeAddressCity, myContact.HomeAddressState, myContact.HomeAddressPostalCode);
                myEmail.Text = myContact.Email1Address;
            }
        }

        #endregion
    }

    public partial class ContactList : Form
    {
        ItemsControl myItemsControl = new ItemsControl();
        public ContactList()
        {
            InitializeComponent();
            VerticalStackPanelHost scrollHost = new VerticalStackPanelHost();
            StackPanel stack = scrollHost.Control;
            stack.HorizontalAlignment = WindowlessControls.HorizontalAlignment.Stretch;
            myHost.Control.Controls.Add(scrollHost);
            myHost.AutoScroll = true;

            scrollHost.Control.Controls.Add(myItemsControl);

            myItemsControl.ContentPresenter = typeof(ContactPresenter);
            myItemsControl.Control = new StackPanel();

            OutlookSession session = new OutlookSession();
            foreach (Contact contact in session.Contacts.Items)
            {
                myItemsControl.Items.Add(contact);
            }
        }

        private void myCloseMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}