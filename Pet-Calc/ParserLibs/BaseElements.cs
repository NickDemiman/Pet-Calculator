﻿namespace Pet_Calc.ParserLibs
{
    /**
    * \brief Базовый абстрактный класс
    * \details Также как высококачественный прототип будущего проекта предоставляет широкие возможности для новых предложений. Лишь базовые сценарии поведения пользователей своевременно верифицированы.
    */
    public abstract class BaseElements
    {
        public object value; ///< поле объект, нужное для дальнейшего наследования и удобного хранения в коллекциях. Например, если у нас есть класс Car, который наследуется от класса Vehicle. Тогда, при поиске по Car мы можем обратиться к Vehicle, чтобы получить всю информацию о нем. В случае с объектами, нужно передать адрес объекта, чтобы вызвать метод GetClass (). В C# нет наследования. Это означает, что каждый объект имеет свой собственный уникальный адрес, и нет возможности обратиться к нему по имени. Но существует множество способов сделать это. Один из таких способов - это использование ссылок.
    }

    /**
     * \brief Абстрактный класс-наследник базового класса
     * \details Данный класс нужен для определения базового класса операнды
     */
    public abstract class BaseOperand : BaseElements
    {

    }

    /**
     * \brief Абстрактный класс-наследник базового класса
     * \details Данный класс нужен для определения базового класса оператора
     */
    public abstract class BaseOperator : BaseElements
    {
        public byte _priority; ///< Byte поле преоритета оператора при вычислении

        /**
         * \brief Перегрузка оператора эквивалентности ==
         * \param obj объект, стоящий справа от оператора, передается в функцию
         * \return Возвращает bool значение
         */
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is BaseOperator))
                return false;
            else
                return (char)this.value == (char)((BaseOperator)obj).value;
        }

        /**
         * \brief Перегрузка функции хеширования объекта
         * \return Возвращает хеш-код от int
         */
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
