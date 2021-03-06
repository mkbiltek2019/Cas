﻿using System;
using System.ComponentModel;
using System.Windows.Forms;
using CAS.UI.Interfaces;
using CAS.UI.UIControls.Auxiliary.Events;
using SmartCore.Entities.General.Atlbs;

namespace CAS.UI.UIControls.AircraftTechnicalLogBookControls
{
    /// <summary>
    /// ЭУ для отображения основной информации о полете
    /// </summary>
    public partial class FlightGeneralInformatonControlLight : EditObjectControl
    {
        ///<summary>
        /// Возвращает редактируемый полет
        ///</summary>
        public AircraftFlight Flight
        {
            get { return AttachedObject as AircraftFlight; }
        }

        #region  public FlightGeneralInformatonControl()
        ///<summary>
        /// Конструктор по умолчанию
        ///</summary>
        public FlightGeneralInformatonControlLight()
        {
            InitializeComponent();
        }
        #endregion

        #region public override void FillControls()
        /// <summary>
        /// Обновляет значения полей
        /// </summary>
        public override void FillControls()
        {

            BeginUpdate();
            if (Flight != null)
            {
                foreach (Control c in Controls)
                {
                    EditObjectControl cc = c as EditObjectControl;
                    if (cc != null) cc.AttachedObject = AttachedObject;
                }
            }
            EndUpdate();
        }
        #endregion

        #region public override bool CheckData()
        /// <summary>
        /// Сохраняет данные текущей директивы
        /// </summary>
        public override bool CheckData()
        {
            // Просматриваем до первой "провалившейся" проверки
            foreach (Control c in Controls)
            {
                EditObjectControl cc = c as EditObjectControl;
                if (cc != null)
                    if (!cc.CheckData()) return false;
            }
            // Все проверки завершились успешно
            return true;
        }

        #endregion

        #region public override void ApplyChanges()
        /// <summary>
        /// Вызывает метод ApplyChanges у каждого контрола
        /// </summary>
        /// <returns></returns>
        public override void ApplyChanges()
        {
            // Просматриваем до первой "провалившейся" проверки
            foreach (Control c in Controls)
            {
                EditObjectControl cc = c as EditObjectControl;
                if (cc != null) cc.ApplyChanges();
            }
        }
        #endregion


        /* 
         *  Реагирование на события
         */

        #region private void FlightDateRouteControl1FlightDateChanget(Auxiliary.Events.DateChangedEventArgs e)

        private void FlightDateRouteControl1FlightDateChanget(DateChangedEventArgs e)
        {
            InvokeFlightDateChanget(e);
        }
        #endregion

        #region private void FlightDateRouteControl1FlightStationFromChanget(ValueChangedEventArgs e)
        private void FlightDateRouteControl1FlightStationFromChanget(ValueChangedEventArgs e)
        {
            InvokeFlightStationFromChanget(e);
        }
        #endregion

        #region private void FlightTimeControl1OutTimeChanget(DateChangedEventArgs e)
        private void FlightTimeControl1OutTimeChanget(DateChangedEventArgs e)
        {
            InvokeOutTimeChanget(e.Date);
        }
        #endregion

        #region private void FlightTimeControl1InTimeChanget(DateChangedEventArgs e)
        private void FlightTimeControl1InTimeChanget(DateChangedEventArgs e)
        {
            InvokeInTimeChanget(e.Date);
        }
        #endregion

        #region private void FlightTimeControl1LDGTimeChanget(DateChangedEventArgs e)
        private void FlightTimeControl1LDGTimeChanget(DateChangedEventArgs e)
        {
            InvokeLDGTimeChanget(e.Date);
        }
        #endregion

        #region private void FlightTimeControl1TakeOffTimeChanget(DateChangedEventArgs e)
        private void FlightTimeControl1TakeOffTimeChanget(DateChangedEventArgs e)
        {
            InvokeTakeOffTimeChanget(e.Date);
        }
        #endregion

        #region private void FlightCrewControl1CrewChanged(CrewChangedEventArgs e)
        private void FlightCrewControl1CrewChanged(CrewChangedEventArgs e)
        {
            InvokeCrewChanged(e);    
        }
        #endregion

        #region Events

        ///<summary>
        /// Возникает во время изменения даты полета
        ///</summary>
        [Category("Flight data")]
        [Description("Возникает в время изменения даты полета")]
        public event DateChangedEventHandler FlightDateChanget;

        ///<summary>
        /// Возникает во время изменения станции отбытия полета
        ///</summary>
        [Category("Flight data")]
        [Description("Возникает во время изменения станции отбытия полета")]
        public event ValueChangedEventHandler FlightStationFromChanget;

        ///<summary>
        /// Возникает при изменении времени вывода из ангара
        ///</summary>
        [Category("Flight data")]
        [Description("Возникает при изменении времени вывода из ангара")]
        public event DateChangedEventHandler OutTimeChanget;

        ///<summary>
        /// Возникает при изменении времени ввода в ангар
        ///</summary>
        [Category("Flight data")]
        [Description("Возникает при изменении времени ввода в ангар")]
        public event DateChangedEventHandler InTimeChanget;

        ///<summary>
        /// Возникает при изменении времени взлета
        ///</summary>
        [Category("Flight data")]
        [Description("Возникает при изменении времени взлета")]
        public event DateChangedEventHandler TakeOffTimeChanget;

        ///<summary>
        /// Возникает при изменении времени посадки
        ///</summary>
        [Category("Flight data")]
        [Description("Возникает при изменении времени посадки")]
        public event DateChangedEventHandler LDGTimeChanget;

        /// <summary>
        /// Событие, возникающее при изменении состава экипажа
        /// </summary>
        [Category("Crew data")]
        [Description("Событие, возникающее при изменении экипажа")]
        public event CrewChangedEventHandler CrewChanged;

        ///<summary>
        /// Сигнализирует об изменени даты полета
        ///</summary>
        ///<param name="e"></param>
        private void InvokeFlightDateChanget(DateChangedEventArgs e)
        {
            DateChangedEventHandler handler = FlightDateChanget;
            if (handler != null) handler(e);
        }

        ///<summary>
        /// Сигнализирует об изменени даты полета
        ///</summary>
        ///<param name="e"></param>
        private void InvokeFlightStationFromChanget(ValueChangedEventArgs e)
        {
            ValueChangedEventHandler handler = FlightStationFromChanget;
            if (handler != null) handler(e);
        }

        ///<summary>
        /// Сигнализирует об изменении времени вывода из ангара
        ///</summary>
        ///<param name="e"></param>
        private void InvokeOutTimeChanget(DateTime e)
        {
            DateChangedEventHandler handler = OutTimeChanget;
            if (handler != null) handler(new DateChangedEventArgs(e));
        }

        ///<summary>
        /// Сигнализирует об изменении времени взлета
        ///</summary>
        ///<param name="e"></param>
        private void InvokeInTimeChanget(DateTime e)
        {
            DateChangedEventHandler handler = InTimeChanget;
            if (handler != null) handler(new DateChangedEventArgs(e));
        }

        ///<summary>
        /// Сигнализирует об изменении времени ввода в ангар
        ///</summary>
        ///<param name="e"></param>
        private void InvokeTakeOffTimeChanget(DateTime e)
        {
            DateChangedEventHandler handler = TakeOffTimeChanget;
            if (handler != null) handler(new DateChangedEventArgs(e));
        }

        ///<summary>
        /// Сигнализирует об изменении времени посадки
        ///</summary>
        ///<param name="e"></param>
        private void InvokeLDGTimeChanget(DateTime e)
        {
            DateChangedEventHandler handler = LDGTimeChanget;
            if (handler != null) handler(new DateChangedEventArgs(e));
        }
        
        /// <summary>
        /// Сигнализирует об изменении экипажа
        /// </summary>
        private void InvokeCrewChanged(CrewChangedEventArgs e)
        {
            if (CrewChanged != null) CrewChanged(e);
        }

        #endregion
    }
}
