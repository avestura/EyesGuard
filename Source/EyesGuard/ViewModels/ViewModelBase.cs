using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace EyesGuard.ViewModels
{
    /// <summary>
    /// Base class for all the viewmodels.
    /// Implements INotifyPropertyChanged
    /// main idea from http://web.archive.org/web/20121105051845/http://dotnet-forum.de/blogs/thearchitect/archive/2012/11/01/die-optimale-implementierung-des-inotifypropertychanged-interfaces.aspx
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        private readonly Dictionary<string, object> propertyValueStorage;


        public ViewModelBase()
        {
            this.propertyValueStorage = new Dictionary<string, object>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected virtual bool SetField<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;

            storage = value;

            this.OnPropertyChanged(propertyName);

            return true;
        }

        protected bool SetValue<T>(Expression<Func<T>> property, T value)
        {
            if (!(property is LambdaExpression lambdaExpression))
            {
                throw new ArgumentException("Invalid lambda expression", "Lambda expression return value can't be null");
            }

            string propertyName = this.GetPropertyName(lambdaExpression);

            T storedValue = this.GetValue<T>(propertyName);

            if (EqualityComparer<T>.Default.Equals(storedValue, value))
                return false;

            this.propertyValueStorage[propertyName] = value;
            this.OnPropertyChanged(propertyName);

            return true;

        }

        protected T GetValue<T>(Expression<Func<T>> property)
        {
            if (!(property is LambdaExpression lambdaExpression))
            {
                throw new ArgumentException("Invalid lambda expression", "Lambda expression return value can't be null");
            }

            string propertyName = this.GetPropertyName(lambdaExpression);

            return GetValue<T>(propertyName);
        }

        private T GetValue<T>(string propertyName)
        {

            if (propertyValueStorage.TryGetValue(propertyName, out object value))
            {
                return (T)value;
            }
            else
            {
                return default;
            }
        }

        private string GetPropertyName(LambdaExpression lambdaExpression)
        {
            MemberExpression memberExpression;

            if (lambdaExpression.Body is UnaryExpression)
            {
                var unaryExpression = lambdaExpression.Body as UnaryExpression;
                memberExpression = unaryExpression.Operand as MemberExpression;
            }
            else
            {
                memberExpression = lambdaExpression.Body as MemberExpression;
            }

            return memberExpression.Member.Name;
        }

    }
}
