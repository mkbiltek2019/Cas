﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CAS.UI.UIControls.Auxiliary;
using CASTerms;
using MetroFramework.Forms;
using SmartCore.Entities.Dictionaries;
using SmartCore.Entities.General;
using SmartCore.Entities.General.Accessory;
using SmartCore.Filters;
using SmartCore.Purchase;

namespace CAS.UI.UIControls.PurchaseControls.Purchase
{
    public partial class PurchaseOrderForm : MetroForm
    {
		#region Fields

		private List<PurchaseRequestRecord> _addedRecord = new List<PurchaseRequestRecord>();

		private PurchaseOrder _order;

		#endregion

		#region Constructor

		public PurchaseOrderForm()
	    {
		    InitializeComponent();
		}

		public PurchaseOrderForm(PurchaseOrder order):this()
		{
			_order = order;

			Task.Run(() => DoWork())
				.ContinueWith(task => Completed(), TaskScheduler.FromCurrentSynchronizationContext());
		}

		#endregion

		#region private void Completed()

		private void Completed()
		{
			UpdateControls();
			UpdateInitialControls();
			purchaseRecordListView1.SetItemsArray(_addedRecord.ToArray());
		}

		#endregion

		#region private void DoWork()

		private void DoWork()
		{

			var records = GlobalObjects.CasEnvironment.Loader.GetObjectList<PurchaseRequestRecord>(new ICommonFilter[]{new CommonFilter<int>(PurchaseRequestRecord.ParentPackageIdProperty, _order.ItemId) });
			var ids = records.Select(s => s.SupplierId).Distinct().ToArray();
			var productIds = records.Select(s => s.PackageItemId).Distinct().ToArray();
			var suppliers = GlobalObjects.CasEnvironment.Loader.GetObjectList<Supplier>(new ICommonFilter[]{new CommonFilter<int>(BaseEntityObject.ItemIdProperty, SmartCore.Filters.FilterType.In, ids), });
			var products = GlobalObjects.CasEnvironment.Loader.GetObjectList<Product>(new ICommonFilter[]{new CommonFilter<int>(BaseEntityObject.ItemIdProperty, SmartCore.Filters.FilterType.In, productIds), });

			foreach (var record in records)
			{
				record.Product = products.FirstOrDefault(i => i.ItemId == record.PackageItemId);
				record.Supplier = suppliers.FirstOrDefault(i => i.ItemId == record.SupplierId);
			}

			_addedRecord.AddRange(records);
		}

		#endregion

		#region private void UpdateInitialControls()

		private void UpdateInitialControls()
		{
			comboBoxStatus.Items.Clear();
			comboBoxStatus.DataSource = Enum.GetValues(typeof(WorkPackageStatus));
			comboBoxStatus.SelectedItem = _order.Status;

			textBoxTitle.Text = _order.Title;
			metroTextBoxNumber.Text = _order.Number;
			dateTimePickerOpeningDate.Value = _order.OpeningDate;
			dateTimePickerClosingDate.Value = _order.ClosingDate;
			dateTimePickerPublishDate.Value = _order.PublishingDate;
			textBoxClosingBy.Text = _order.CloseByUser;
			textBoxPublishedBy.Text = _order.PublishedByUser;
			textBoxAuthor.Text = GlobalObjects.CasEnvironment.IdentityUser.ToString();
			textBoxRemarks.Text = _order.Remarks;
		}

		#endregion

		#region private void UpdateControls()

		private void UpdateControls()
		{
			comboBoxMeasure.Items.Clear();
			comboBoxMeasure.Items.AddRange(Measure.GetByCategories(new[] { MeasureCategory.Mass, MeasureCategory.EconomicEntity }));

			comboBoxCondition.Items.Clear();
			comboBoxCondition.DataSource = Enum.GetValues(typeof(ComponentStatus));

			comboBoxCurrency.Items.Clear();
			comboBoxCurrency.Items.AddRange(Сurrency.Items.ToArray());
		}

		#endregion

		#region private void ButtonDelete_Click(object sender, EventArgs e)

		private void ButtonDelete_Click(object sender, EventArgs e)
		{
			if (purchaseRecordListView1.SelectedItems.Count == 0) return;

			DialogResult confirmResult =
				MessageBox.Show(
						"Do you really want to delete Purchase Record(s)? ", "Confirm delete operation",
					MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

			if (confirmResult == DialogResult.Yes)
			{
				foreach (var item in purchaseRecordListView1.SelectedItems.ToArray())
				{
					_addedRecord.Remove(item);
					GlobalObjects.CasEnvironment.Keeper.Delete(item, true);
				}

				purchaseRecordListView1.SetItemsArray(_addedRecord.ToArray());
			}	
		}

		#endregion

		#region private void ButtonCancel_Click(object sender, EventArgs e)

		private void ButtonCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		#endregion

		#region private void NumericUpDownQuantity_ValueChanged(object sender, EventArgs e)

		private void NumericUpDownQuantity_ValueChanged(object sender, EventArgs e)
		{
			SetForMeasure();
		}

		#endregion

		#region private void SetForMeasure()
		/// <summary>
		/// Изменяет контрол в соотствествии с выбранной единицей измерения
		/// </summary>
		private void SetForMeasure()
		{
			var measure = comboBoxMeasure.SelectedItem as Measure;
			if (measure == null || measure.MeasureCategory != MeasureCategory.Mass)
				numericUpDownQuantity.DecimalPlaces = 0;
			else numericUpDownQuantity.DecimalPlaces = 2;

			var quantity = numericUpDownQuantity.Value;

			textBoxTotal.Text = $"{quantity:0.##}" + (measure != null ? " " + measure + "(s)" : "");
		}
		#endregion

		#region private void ComboBoxMeasure_SelectedIndexChanged(object sender, EventArgs e)

		private void ComboBoxMeasure_SelectedIndexChanged(object sender, EventArgs e)
		{
			SetForMeasure();
		}

		#endregion

		#region private void ComboBoxCondition_SelectedIndexChanged(object sender, EventArgs e)

		private void ComboBoxCondition_SelectedIndexChanged(object sender, EventArgs e)
		{
			if(comboBoxCondition.SelectedItem == null) return;
			if (purchaseRecordListView1.SelectedItem == null) return;

			//var selected = (ComponentStatus)comboBoxCondition.SelectedItem;

			//if (selected == ComponentStatus.New)
			//{
			//	numericUpDown1.Value = purchaseRecordListView1.SelectedItem.Price.CostNew;
			//	comboBoxCurrency.SelectedItem = purchaseRecordListView1.SelectedItem.Price.СurrencyNew;
			//}
			//else if (selected == ComponentStatus.Overhaul)
			//{
			//	numericUpDown1.Value = purchaseRecordListView1.SelectedItem.Price.CostOverhaul;
			//	comboBoxCurrency.SelectedItem = purchaseRecordListView1.SelectedItem.Price.СurrencyOH;
			//}
			//else if (selected == ComponentStatus.Repair)
			//{
			//	numericUpDown1.Value = purchaseRecordListView1.SelectedItem.Price.CostRepair;
			//	comboBoxCurrency.SelectedItem = purchaseRecordListView1.SelectedItem.Price.СurrencyRepair;
			//}
			//else if (selected == ComponentStatus.Serviceable)
			//{
			//	numericUpDown1.Value = purchaseRecordListView1.SelectedItem.Price.CostServiceable;
			//	comboBoxCurrency.SelectedItem = purchaseRecordListView1.SelectedItem.Price.СurrencyServ;
			//}

		}

		#endregion

		#region private void PurchaseRecordListView1_SelectedItemsChanged(object sender, SelectedItemsChangeEventArgs e)

		private void PurchaseRecordListView1_SelectedItemsChanged(object sender, SelectedItemsChangeEventArgs e)
		{
			button1.Enabled = purchaseRecordListView1.SelectedItem != null;
			if (purchaseRecordListView1.SelectedItem == null) return;

			comboBoxCondition.SelectedItem = purchaseRecordListView1.SelectedItem.CostCondition;
			comboBoxMeasure.SelectedItem = purchaseRecordListView1.SelectedItem.Measure;
			numericUpDownQuantity.Value = (decimal)purchaseRecordListView1.SelectedItem.Quantity;
		}
		#endregion

		#region private void Button1_Click(object sender, EventArgs e)

		private void Button1_Click(object sender, EventArgs e)
		{
			if (purchaseRecordListView1.SelectedItem == null) return;

			purchaseRecordListView1.SelectedItem.CostCondition = (ComponentStatus) comboBoxCondition.SelectedItem;
			purchaseRecordListView1.SelectedItem.Measure = (Measure) comboBoxMeasure.SelectedItem;
			purchaseRecordListView1.SelectedItem.Quantity = (double) numericUpDownQuantity.Value;
			purchaseRecordListView1.SelectedItem.Cost = (double)numericUpDown1.Value;
			purchaseRecordListView1.SelectedItem.Currency = (Сurrency) comboBoxCurrency.SelectedItem;

			purchaseRecordListView1.SetItemsArray(_addedRecord.ToArray());
		}

		#endregion

		#region private void Reset()

		private void Reset()
		{
			button1.Enabled = false;
			comboBoxCondition.SelectedItem = null;
			comboBoxMeasure.SelectedItem = null;
			numericUpDownQuantity.Value = 0;
			numericUpDown1.Value = 0;
		}

		#endregion

		#region private void ButtonOk_Click(object sender, EventArgs e)

		private void ButtonOk_Click(object sender, EventArgs e)
		{
			if (textBoxTitle.Text == "")
			{
				MessageBox.Show("Please, enter a Title", (string)new GlobalTermsProvider()["SystemName"],
					MessageBoxButtons.OK,
					MessageBoxIcon.Exclamation);
				return;
			}

			if (purchaseRecordListView1.ListViewItemList.Count <= 0)
			{
				MessageBox.Show("Please select a price for purchase order", (string)new GlobalTermsProvider()["SystemName"],
					MessageBoxButtons.OK,
					MessageBoxIcon.Exclamation);
				return;
			}
			//запись новой информации в запросный ордер
			ApplyPurchaseData();
			//сохранение запросного ордера
			GlobalObjects.CasEnvironment.NewKeeper.Save(_order);

			foreach (var record in _addedRecord)
			{
				GlobalObjects.CasEnvironment.NewKeeper.Save(record);
			}

			DialogResult = DialogResult.OK;
		}

		#endregion

		#region private void ApplyOrderData()
		private void ApplyPurchaseData()
		{
			_order.Title = textBoxTitle.Text;
			_order.Number = metroTextBoxNumber.Text;
			_order.Status = (WorkPackageStatus)comboBoxStatus.SelectedItem;
			_order.Remarks = textBoxRemarks.Text;

			if (_order.ItemId <= 0)
				_order.Author = GlobalObjects.CasEnvironment.IdentityUser.ToString();

			if (_order.Status == WorkPackageStatus.All)
			{
				_order.OpeningDate = dateTimePickerOpeningDate.Value;
				_order.ClosingDate = dateTimePickerClosingDate.Value;
				_order.PublishingDate = dateTimePickerPublishDate.Value;
			}
			else if (_order.Status == WorkPackageStatus.Opened)
			{
				_order.OpeningDate = dateTimePickerOpeningDate.Value;
			}
			else if (_order.Status == WorkPackageStatus.Closed)
			{
				_order.ClosingDate = dateTimePickerClosingDate.Value;
			}
			else if (_order.Status == WorkPackageStatus.Published)
			{
				_order.PublishingDate = dateTimePickerPublishDate.Value;
			}
		}
		#endregion
	}
}