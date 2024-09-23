using ClassDemoPizzaLib.model;

namespace ClassDemoPizzaRest.model
{
    public static class PizzaConverter
    {

        public static IPizza PizzaDto2Pizza(PizzaDTO dto)
        {
            IPizza pizza = new Pizza1();
            pizza.Id = dto.Id;
            pizza.Name = dto.Name;          // eventuelt en ArgumentException
            pizza.Description = dto.Description;// eventuelt en ArgumentException
            pizza.Price = dto.Price;    // eventuelt en ArgumentException

            return pizza;
        }
    }
}
