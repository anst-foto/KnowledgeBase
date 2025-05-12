// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;
using System.Linq;


namespace KnowledgeBase.Model;

public partial class Article : IEquatable<Article>
{
    /// <summary>
    /// Метод сравнения двух объектов Article
    /// </summary>
    /// <param name="other">
    /// Объект, с которым происходит сравнение
    ///</param>
    /// <returns>
    /// true, если объекты равны; false, если объекты не равны
    /// </returns>
    public bool Equals(Article? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Title == other.Title
               && Tags.OrderBy(t => t).SequenceEqual(other.Tags.OrderBy(t => t))
               && Content == other.Content
               && DateOfCreation.Equals(other.DateOfCreation)
               && DateOfLastUpdate.Equals(other.DateOfLastUpdate)
               && IsDeleted == other.IsDeleted;
    }

    /// <summary>
    /// Переопределение метода Equals() для класса
    /// </summary>
    /// <param name="obj">Объект, с которым происходит сравнение</param>
    /// <returns>Результат сравнения</returns>
    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Article)obj);
    }

    /// <summary>
    /// Вычисление хеш-кода объекта
    /// </summary>
    /// <returns>Хеш-код объекта</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(Title, Tags, Content, DateOfCreation, DateOfLastUpdate, IsDeleted);
    }

    /// <summary>
    /// Сравнение объектов класса Article на равенство
    /// </summary>
    /// <param name="left">Левый объект</param>
    /// <param name="right">Правый объект</param>
    /// <returns>Результат сравнения</returns>
    public static bool operator ==(Article? left, Article? right)
    {
        return Equals(left, right);
    }

    /// <summary>
    /// Сравнение объектов класса Article на неравенство
    /// </summary>
    /// <param name="left">Левый объект</param>
    /// <param name="right">Правый объект</param>
    /// <returns>Результат сравнения</returns>
    public static bool operator !=(Article? left, Article? right)
    {
        return !Equals(left, right);
    }
}