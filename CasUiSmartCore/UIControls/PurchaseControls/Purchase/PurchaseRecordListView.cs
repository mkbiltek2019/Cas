﻿using System.Collections.Generic;
using CAS.UI.UIControls.NewGrid;
using CASTerms;
using SmartCore.Entities.Dictionaries;
using SmartCore.Purchase;

namespace CAS.UI.UIControls.PurchaseControls.Purchase
{
	public partial class PurchaseRecordListView : BaseGridViewControl<PurchaseRequestRecord>
	{
		private readonly bool _orderBySupplies;

		#region Constructor

		public PurchaseRecordListView(bool orderBySupplies = false)
		{
			_orderBySupplies = orderBySupplies;
			InitializeComponent();
			OldColumnIndex = 2;
			SortMultiplier = 1;
		}

		#endregion

		#region protected override void SetHeaders()

		protected override void SetHeaders()
		{
			AddColumn("Supplier", (int)(radGridView1.Width * 0.2f));
			AddColumn("Q-ty", (int)(radGridView1.Width * 0.2f));
			AddColumn("Cost", (int)(radGridView1.Width * 0.2f));
			AddColumn("Condition", (int)(radGridView1.Width * 0.2f));
			AddColumn("Measure", (int)(radGridView1.Width * 0.2f));
			AddColumn("Product", (int)(radGridView1.Width * 0.2f));
			AddColumn("IncoTerm", (int)(radGridView1.Width * 0.2f));
			AddColumn("Designation", (int)(radGridView1.Width * 0.2f));
			AddColumn("PayTerm", (int)(radGridView1.Width * 0.2f));
			AddColumn("BruttoWeight", (int)(radGridView1.Width * 0.2f));
			AddColumn("CargoVolume", (int)(radGridView1.Width * 0.2f));
			AddColumn("NettoWeight", (int)(radGridView1.Width * 0.2f));
			AddColumn("ShipCompany", (int)(radGridView1.Width * 0.2f));
			AddColumn("ShipTo", (int)(radGridView1.Width * 0.2f));
			AddColumn("Signer", (int)(radGridView1.Width * 0.3f));
		}

		#endregion

		#region protected override ListViewItem.ListViewSubItem[] GetListViewSubItems(Product item)

		protected override List<CustomCell> GetListViewSubItems(PurchaseRequestRecord item)
		{
			var author = GlobalObjects.CasEnvironment.GetCorrector(item);
			var destiantion = "";
			if (item?.ParentInitialRecord?.DestinationObjectType == SmartCoreType.Aircraft)
				destiantion = GlobalObjects.AircraftsCore.GetAircraftById(item?.ParentInitialRecord?.DestinationObjectId ?? -1)?.ToString();
			else destiantion = GlobalObjects.StoreCore.GetStoreById(item?.ParentInitialRecord?.DestinationObjectId ?? -1)?.ToString();

			var temp = $"{item?.Product?.PartNumber} | Q-ty:{item?.ParentInitialRecord?.Quantity} | Reason: {item?.ParentInitialRecord?.InitialReason} | Destination: {destiantion} | Priority: {item?.ParentInitialRecord?.Priority}";
			return new List<CustomCell>()
			{
				CreateRow(item.Supplier.ToString(),item.Supplier),
				CreateRow(item.Quantity.ToString(),item.Quantity),
				CreateRow(item.Cost.ToString(),item.Cost),
				CreateRow(item.CostCondition.ToString(),item.CostCondition),
				CreateRow(item.Measure.ToString(),item.Measure),
				CreateRow(temp,temp),
				CreateRow(item.IncoTerm.ToString(),item.IncoTerm),
				CreateRow(item.Designation.ToString(),item.Designation),
				CreateRow(item.PayTerm.ToString(),item.PayTerm),
				CreateRow(item.BruttoWeight.ToString(),item.BruttoWeight),
				CreateRow(item.CargoVolume.ToString(),item.CargoVolume),
				CreateRow(item.NettoWeight.ToString(),item.NettoWeight),
				CreateRow(item.ShipCompany?.ToString(),item.ShipCompany),
				CreateRow(item.ShipTo.ToString(),item.ShipTo),
				CreateRow(author,author),
			};
		}

		#endregion

		#region Overrides of BaseGridViewControl<InitialOrderRecord>

		protected override void GroupingItems()
		{
			Grouping("Product");
		}

		#endregion

	}
}
