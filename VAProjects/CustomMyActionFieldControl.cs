using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace VAProjects
{
     public class CustomMyActionFieldControl: BaseFieldControl
    {
        CustomMyActionField field;
        
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }
        public CustomMyActionFieldControl(CustomMyActionField parentField)
        {
            this.field = parentField;
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            if (this.Field != null && this.ControlMode != SPControlMode.Display)
            {
               
                this.field.Choices.Clear();
                this.field.Choices.Add (SPContext.Current.Web.CurrentUser.Name);
               
            }
        }
       
        
        public override void Focus()
        {
            EnsureChildControls();
         }
    }
}
