using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Functions.Base
{
    public static class ExpressionHelper
    {
        public static Expression<Func<TTo, bool>> Convert<TFrom, TTo>(Expression<Func<TFrom, bool>> expr)
        {
            Dictionary<Expression, Expression> substitutues = new Dictionary<Expression, Expression>();
            var oldParam = expr.Parameters[0];
            var newParam = Expression.Parameter(typeof(TTo), oldParam.Name);
            substitutues.Add(oldParam, newParam);

            Expression body = ConvertNode(expr.Body, substitutues);
            return Expression.Lambda<Func<TTo, bool>>(body, newParam);
        }

        public static Expression<Func<TTo, object>> Convert<TFrom, TTo>(Expression<Func<TFrom, object>> expr)
        {
            Dictionary<Expression, Expression> substitutues = new Dictionary<Expression, Expression>();
            var oldParam = expr.Parameters[0];
            var newParam = Expression.Parameter(typeof(TTo), oldParam.Name);
            substitutues.Add(oldParam, newParam);

            Expression body = ConvertNode(expr.Body, substitutues);
            return Expression.Lambda<Func<TTo, object>>(body, newParam);
        }
        static Expression ConvertNode(Expression node, IDictionary<Expression, Expression> subst)
        {
            if (node == null) return null;
            if (subst.ContainsKey(node)) return subst[node];

            switch (node.NodeType)
            {
                case ExpressionType.Constant:
                    return node;
                case ExpressionType.MemberAccess:
                    {
                        var me = (MemberExpression)node;
                        var newNode = ConvertNode(me.Expression, subst);
                        return Expression.MakeMemberAccess(newNode, newNode.Type.GetMember(me.Member.Name).Single());
                    }
                case ExpressionType.Equal: // == 
                    {
                        var be = (BinaryExpression)node;
                        return Expression.Equal(ConvertNode(be.Left, subst), ConvertNode(be.Right, subst), be.IsLiftedToNull, be.Method);
                    }
                case ExpressionType.NotEqual: // != 
                    {
                        var be = (BinaryExpression)node;
                        return Expression.NotEqual(ConvertNode(be.Left, subst), ConvertNode(be.Right, subst), be.IsLiftedToNull, be.Method);
                    }
                case ExpressionType.AndAlso: // &&
                    {
                        var be = (BinaryExpression)node;
                        return Expression.AndAlso(ConvertNode(be.Left, subst), ConvertNode(be.Right, subst), be.Method);
                    }
                case ExpressionType.And: // &
                    {
                        var be = (BinaryExpression)node;
                        return Expression.And(ConvertNode(be.Left, subst), ConvertNode(be.Right, subst), be.Method);
                    }
                case ExpressionType.OrElse: // ||
                    {
                        var be = (BinaryExpression)node;
                        return Expression.OrElse(ConvertNode(be.Left, subst), ConvertNode(be.Right, subst), be.Method);
                    }
                case ExpressionType.Or: // |
                    {
                        var be = (BinaryExpression)node;
                        return Expression.Or(ConvertNode(be.Left, subst), ConvertNode(be.Right, subst), be.Method);
                    }
                case ExpressionType.GreaterThan: // >
                    {
                        var be = (BinaryExpression)node;
                        return Expression.GreaterThan(ConvertNode(be.Left, subst), ConvertNode(be.Right, subst));
                    }
                case ExpressionType.GreaterThanOrEqual: // >=
                    {
                        var be = (BinaryExpression)node;
                        return Expression.GreaterThanOrEqual(ConvertNode(be.Left, subst), ConvertNode(be.Right, subst));
                    }
                case ExpressionType.LessThan: // <
                    {
                        var be = (BinaryExpression)node;
                        return Expression.LessThan(ConvertNode(be.Left, subst), ConvertNode(be.Right, subst));
                    }
                case ExpressionType.LessThanOrEqual: // <=
                    {
                        var be = (BinaryExpression)node;
                        return Expression.LessThanOrEqual(ConvertNode(be.Left, subst), ConvertNode(be.Right, subst));
                    }
                case ExpressionType.Not: // !
                    {
                        var be = (UnaryExpression)node;
                        return Expression.Not(ConvertNode(be.Operand, subst), be.Method);
                    }
                case ExpressionType.Call: // inside linq Contains, Startwith, etc.
                    {
                        var be = (MethodCallExpression)node;

                        return Expression.Call(be.Object, be.Method, be.Arguments);
                    }
                case ExpressionType.Convert: // 
                    {
                        var be = (UnaryExpression)node;

                        return Expression.Convert(ConvertNode(be.Operand, subst), be.Type);
                    }
                default:
                    throw new NotSupportedException(node.NodeType.ToString());
            }
        }
        public static string ConvertString<TFrom, TTo>(Expression<Func<TFrom, object>> expr)
        {
            string body = ConvertNodeToString(expr.Body);
            return body;
        }

        static string ConvertNodeToString(Expression node)
        {
            if (node.NodeType == ExpressionType.Parameter)
            {
                return null;
            }
            else
            {
                var s = ConvertNodeToString(((MemberExpression)node).Expression);
                if (s != null)
                    return s + "." + ((MemberExpression)node).Member.Name;
                return ((MemberExpression)node).Member.Name;
            }
        }
    }
}
