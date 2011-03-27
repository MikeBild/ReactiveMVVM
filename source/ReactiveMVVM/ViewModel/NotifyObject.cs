using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace ReactiveMVVM.ViewModel
{
    public abstract class NotifyObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected void NotifyPropertyChanged(string propertyName)
        {
            VerifyPropertyName(propertyName);

            var handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        protected void NotifyPropertyChanged<TProperty>(Expression<Func<TProperty>> property)
        {
            var lambda = (LambdaExpression)property;

            MemberExpression memberExpression;

            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = (UnaryExpression)lambda.Body;
                memberExpression = (MemberExpression)unaryExpression.Operand;
            }
            else memberExpression = (MemberExpression)lambda.Body;

            NotifyPropertyChanged(memberExpression.Member.Name);
        }

        [Conditional("DEBUG")]
        public void VerifyPropertyName(string propertyName)
        {
            if (String.IsNullOrEmpty(propertyName))
                return;

            if (GetType().GetProperties().Where(p => p.Name == propertyName).FirstOrDefault() == null)
            {
                throw new ArgumentException(String.Format("Invalid property name: {0}", propertyName));
            }
        }
    }
}