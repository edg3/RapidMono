﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LocalDB
{
    using System.Linq;
    using System.Data;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Linq.Expressions;
    using System.ComponentModel;
    using System;


    public partial class ScoresDataContext : IDisposable
    {


        #region Extensibility Method Definitions
        partial void OnCreated();

        public void Dispose()
        {
            // TODO: dispose
        }

        internal bool DatabaseExists()
        {
            return false;
        }

        internal void CreateDatabase()
        {
            return;
        }
        #endregion

        public ScoresDataContext(string connection)
        {
            OnCreated();
        }

        public List<Score> Scores
        {
            get
            {
                return new();
            }
        }
    }

    public partial class Score : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private int _Score1;

        private System.DateTime _When;

        private int _Id;

        #region Extensibility Method Definitions
        partial void OnLoaded();
        //partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void OnScore1Changing(int value);
        partial void OnScore1Changed();
        partial void OnWhenChanging(System.DateTime value);
        partial void OnWhenChanged();
        partial void OnIdChanging(int value);
        partial void OnIdChanged();
        #endregion

        public Score()
        {
            OnCreated();
        }

        public int Score1
        {
            get
            {
                return this._Score1;
            }
            set
            {
                if ((this._Score1 != value))
                {
                    this.OnScore1Changing(value);
                    this.SendPropertyChanging();
                    this._Score1 = value;
                    this.SendPropertyChanged("Score1");
                    this.OnScore1Changed();
                }
            }
        }

        public System.DateTime When
        {
            get
            {
                return this._When;
            }
            set
            {
                if ((this._When != value))
                {
                    this.OnWhenChanging(value);
                    this.SendPropertyChanging();
                    this._When = value;
                    this.SendPropertyChanged("When");
                    this.OnWhenChanged();
                }
            }
        }

        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                if ((this._Id != value))
                {
                    this.OnIdChanging(value);
                    this.SendPropertyChanging();
                    this._Id = value;
                    this.SendPropertyChanged("Id");
                    this.OnIdChanged();
                }
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
            if ((this.PropertyChanging != null))
            {
                this.PropertyChanging(this, emptyChangingEventArgs);
            }
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
#pragma warning restore 1591