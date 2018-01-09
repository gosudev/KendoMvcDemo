using System;
using System.Collections.Generic;
using System.Linq;
using KendoMvcDemo.Core.ViewModels;

namespace KendoMvcDemo.Core.Services
{
    public class ComplaintService
    {
        public IList<ComplaintViewModel> GetAll()
        {
            return Enumerable.Range(1, 50).Select(x => new ComplaintViewModel()
            {
                Id = x,
                Title = $"Complaint nr: {x}",
                ConsumerNarrative = $"{x} Lorem ipsum dolor sit amet,sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat,  At vero eos et accusam et justo duo dolores et ea rebum.  Lorem ipsum dolor sit amet,  no sea takimata sanctus est Lorem ipsum dolor sit amet.  Stet clita kasd gubergren,  no sea takimata sanctus est Lorem ipsum dolor sit amet.  no sea takimata sanctus est Lorem ipsum dolor sit amet.  no sea takimata sanctus est Lorem ipsum dolor sit amet.  sed diam voluptua.  ",
                SentDate = DateTime.Now.AddDays(-x)

            }).ToList();
        }
    }
}
