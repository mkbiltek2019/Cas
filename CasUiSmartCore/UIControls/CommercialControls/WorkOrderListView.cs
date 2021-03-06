﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CAS.UI.Interfaces;
using CAS.UI.Management.Dispatchering;
using CAS.UI.UIControls.NewGrid;
using SmartCore.Entities.General.Commercial;

namespace CAS.UI.UIControls.CommercialControls
{
    ///<summary>
    /// список для отображения ордеров запроса
    ///</summary>
    public partial class WorkOrderListView : BaseGridViewControl<WorkOrder>
    {
        #region Fields

        #endregion

        #region Constructors

        #region public WorkOrderListView()
        ///<summary>
        ///</summary>
        public WorkOrderListView()
        {
            InitializeComponent();
        }
        #endregion

        #endregion

        #region Methods

        #region protected override List<PropertyInfo> GetTypeProperties()
        protected override List<PropertyInfo> GetTypeProperties()
        {
            List<PropertyInfo> props = base.GetTypeProperties();
            PropertyInfo prop = props.FirstOrDefault(p => p.Name.ToLower() == "aircraft");
            props.Remove(prop);

            return props;
        }
		#endregion

		#region protected override void SetHeaders()
		///// <summary>
		///// Устанавливает заголовки
		///// </summary>
		//protected override void SetHeaders()
		//{
		//}
		#endregion

		#region protected override SetGroupsToItems(int columnIndex)
		//protected override void SetGroupsToItems(int columnIndex)
  //      {
  //          itemsListView.Groups.Clear();
  //          itemsListView.Groups.Add("GroupOpened", "Opened");
  //          itemsListView.Groups.Add("GroupPublished", "Published");
  //          itemsListView.Groups.Add("GroupClosed", "Closed");
  //          itemsListView.Groups.Add("GroupUnk", "Unknown");

  //          foreach (ListViewItem item in ListViewItemList)
  //          {
  //              switch (((WorkOrder)item.Tag).Status)
  //              {
  //                  case WorkPackageStatus.Closed:
  //                      item.Group = itemsListView.Groups[2];
  //                      break;
  //                  case WorkPackageStatus.Published:
  //                      item.Group = itemsListView.Groups[1];
  //                      break;
  //                  case WorkPackageStatus.Opened:
  //                      item.Group = itemsListView.Groups[0];
  //                      break;
  //                  default:
  //                      item.Group = itemsListView.Groups[3];
  //                      break;
  //              }
  //          }
  //      }
        #endregion

        #region protected override void FillDisplayerRequestedParams(ReferenceEventArgs e)

        protected override void FillDisplayerRequestedParams(ReferenceEventArgs e)
        {
            if (SelectedItem != null)
            {
                WorkOrder item = SelectedItem;
                e.TypeOfReflection = ReflectionTypes.DisplayInNew;
                e.DisplayerText = item.Title;
                e.RequestedEntity = new WorkOrderScreen(item);
            }
        }
        #endregion

        #endregion
    }
}
