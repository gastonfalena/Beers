using CRUDBEER.DTO;
using FluentValidation;

namespace CRUDBEER.Validators
{
    public class BeerInsertValidator : AbstractValidator<BeerInsertDto>
    {
        public BeerInsertValidator() {
            //Name ya no permite null por la definciion en el modelo
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es obligatorio");
            //Rango de caracteres min y max
            RuleFor(x=>x.Name).Length(2,20).WithMessage("El nombre debe medir de 2 a 20 caracteres");
            //Marca no deberia ser null
            RuleFor(x=>x.BrandID).NotNull().WithMessage("La marca es obligatorio");
            //id mayor a 0,en el mensaje de error no especificamos para no darle tanta informacion al usuario
            RuleFor(x => x.BrandID).GreaterThan(0).WithMessage("Error con el valor enviado de marca");
            //El grado de alcohol debe ser mayor a 0
            RuleFor(x => x.Alcohol).GreaterThan(0).WithMessage(x => "El {PropertyName} debe ser mayor a 0");
        }
    }
}
