﻿namespace EducationalCourse.Domain.Dtos.Order
{
    public class GetUserOrderForPannelDto
    {
        public GetUserOrderForPannelDto()
        {
            OrderDetails = new List<OrderDetailDto>();
        }
        public int TotalPayment { get; set; }
        public string PaymentDateDisplay { get; set; }

        public List<OrderDetailDto> OrderDetails { get; set; }
    }
}
