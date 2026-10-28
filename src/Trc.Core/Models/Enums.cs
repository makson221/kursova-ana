using System.ComponentModel.DataAnnotations;

namespace Trc.Core.Models;

/// <summary>Роль користувача системи.</summary>
public enum UserRole
{
    [Display(Name = "Адміністратор")] Administrator,
    [Display(Name = "Менеджер")] Manager,
    [Display(Name = "Касир")] Cashier
}

/// <summary>Тип торгового приміщення.</summary>
public enum PremisesType
{
    [Display(Name = "Магазин")] Shop,
    [Display(Name = "Фуд-корт")] FoodCourt,
    [Display(Name = "Ресторан / кафе")] Restaurant,
    [Display(Name = "Розважальна зона")] Entertainment,
    [Display(Name = "Кінозал")] Cinema,
    [Display(Name = "Сфера послуг")] Service,
    [Display(Name = "Склад")] Storage
}

/// <summary>Стан приміщення.</summary>
public enum PremisesStatus
{
    [Display(Name = "Вільне")] Free,
    [Display(Name = "Орендоване")] Occupied,
    [Display(Name = "На ремонті")] Maintenance
}

/// <summary>Категорія діяльності орендаря.</summary>
public enum TenantCategory
{
    [Display(Name = "Одяг та взуття")] Clothing,
    [Display(Name = "Електроніка")] Electronics,
    [Display(Name = "Продукти харчування")] Grocery,
    [Display(Name = "Громадське харчування")] Food,
    [Display(Name = "Розваги")] Entertainment,
    [Display(Name = "Краса та здоров'я")] Beauty,
    [Display(Name = "Послуги")] Services,
    [Display(Name = "Інше")] Other
}

/// <summary>Стан договору оренди.</summary>
public enum LeaseStatus
{
    [Display(Name = "Діючий")] Active,
    [Display(Name = "Розірваний")] Terminated,
    [Display(Name = "Завершений")] Expired
}

/// <summary>Стан платежу за оренду.</summary>
public enum PaymentStatus
{
    [Display(Name = "Очікує оплати")] Pending,
    [Display(Name = "Сплачено")] Paid
}

/// <summary>Фільтр для списку платежів.</summary>
public enum PaymentFilter
{
    [Display(Name = "Усі")] All,
    [Display(Name = "Неоплачені")] Unpaid,
    [Display(Name = "Прострочені")] Overdue,
    [Display(Name = "Сплачені")] Paid
}

/// <summary>Відділ (служба) ТРЦ.</summary>
public enum Department
{
    [Display(Name = "Адміністрація")] Administration,
    [Display(Name = "Служба безпеки")] Security,
    [Display(Name = "Клінінг")] Cleaning,
    [Display(Name = "Технічна служба")] Technical,
    [Display(Name = "Маркетинг")] Marketing,
    [Display(Name = "Паркінг")] Parking
}

/// <summary>Стан розважального заходу.</summary>
public enum EventStatus
{
    [Display(Name = "Заплановано")] Planned,
    [Display(Name = "Відбувся")] Completed,
    [Display(Name = "Скасовано")] Cancelled
}
