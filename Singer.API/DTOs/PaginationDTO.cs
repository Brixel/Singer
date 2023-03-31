using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Singer.Resources;

namespace Singer.DTOs;

/// <summary>
///     Model that represents a page of items. It is used to devide a big list of data in multiple
///     pages and to provide the functionality to navigate to previous/next pages with ease.
/// </summary>
/// <typeparam name="T">Type of the items that are passed with the model.</typeparam>
public class PaginationDTO<T>
{
    /// <summary>Url to the previous page. If this is the first page, the value is <see cref="null"/>.</summary>
    [Required(
        ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
        ErrorMessageResourceType = typeof(ErrorMessages))]
    [StringLength(
       maximumLength: ValidationValues.MaxUrlLength,
       ErrorMessageResourceName = nameof(ErrorMessages.FieldLengthCanMaximumBe),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Url(
       ErrorMessageResourceName = nameof(ErrorMessages.InvalidUrl),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.PreviousPageUrl))]
    public string PreviousPageUrl { get; set; }

    /// <summary>Url to the current page.</summary>
    [Required(
      ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
      ErrorMessageResourceType = typeof(ErrorMessages))]
    [StringLength(
       maximumLength: ValidationValues.MaxUrlLength,
       ErrorMessageResourceName = nameof(ErrorMessages.FieldLengthCanMaximumBe),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Url(
       ErrorMessageResourceName = nameof(ErrorMessages.InvalidUrl),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.CurrentPageUrl))]
    public string CurrentPageUrl { get; set; }

    /// <summary>
    ///     Url to the next page of items. If this is the last page, the value is <see cref="null"/>.
    /// </summary>
    [Required(
      ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
      ErrorMessageResourceType = typeof(ErrorMessages))]
    [StringLength(
       maximumLength: ValidationValues.MaxUrlLength,
       ErrorMessageResourceName = nameof(ErrorMessages.FieldLengthCanMaximumBe),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Url(
       ErrorMessageResourceName = nameof(ErrorMessages.InvalidUrl),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.NextPageUrl))]
    public string NextPageUrl { get; set; }

    /// <summary>The number of items given with this response.</summary>
    [Required(
      ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
      ErrorMessageResourceType = typeof(ErrorMessages))]
    [Range(
       minimum: 0,
       maximum: int.MaxValue,
       ErrorMessageResourceName = nameof(ErrorMessages.FieldMustBeBetween),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.Size))]
    public int Size { get; set; }

    /// <summary>The index at which this section of values is located.</summary>
    [Required(
      ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
      ErrorMessageResourceType = typeof(ErrorMessages))]
    [Range(
       minimum: 0,
       maximum: int.MaxValue,
       ErrorMessageResourceName = nameof(ErrorMessages.FieldMustBeBetween),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.PageIndex))]
    public int PageIndex { get; set; }

    /// <summary>Total number of items in the data base.</summary>
    [Required(
      ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
      ErrorMessageResourceType = typeof(ErrorMessages))]
    [Range(
       minimum: 0,
       maximum: int.MaxValue,
       ErrorMessageResourceName = nameof(ErrorMessages.FieldMustBeBetween),
       ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.TotalSize))]
    public int TotalSize { get; set; }

    /// <summary>The returned items.</summary>
    [Required(
      ErrorMessageResourceName = nameof(ErrorMessages.FieldIsRequired),
      ErrorMessageResourceType = typeof(ErrorMessages))]
    [Display(
       ResourceType = typeof(DisplayNames),
       Name = nameof(DisplayNames.Items))]
    public IReadOnlyList<T> Items { get; set; }
}
