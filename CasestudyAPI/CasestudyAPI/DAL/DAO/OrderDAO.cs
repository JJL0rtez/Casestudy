using CasestudyAPI.DAL.DomainClasses;
using CasestudyAPI.Helpers;
using CaseStudyAPI.DAL.DomainClasses;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace CasestudyAPI.DAL.DAO
{
    public class OrderDAO
    {

        private AppDbContext _db;
        public OrderDAO(AppDbContext ctx)
        {
            _db = ctx;
        }
        public int AddOrder(int customerid, OrderSelectionHelper[] selections)
        {
            int orderId = -1;
            using (_db)
            {
                // we need a transaction as multiple entities involved
                using (var _trans = _db.Database.BeginTransaction())
                {
                    try
                    {
                        Order order = new Order();
                        order.CustomerId = customerid;
                        order.Id = 0;
                        order.OrderAmount = 0;
                        order.OrderDate = System.DateTime.Now;
                        order.OrderLineItems = new List<OrderLineItem>();

                        // calculate the totals and then add the order row to the table
                        foreach (OrderSelectionHelper selection in selections)
                        {
                            Product product = _db.Products.FirstOrDefault(item => item.Id == selection.item.ProductId);
                            if (selection.Qty < product.QtyOnHand)
                            {
                                product.QtyOnHand -= selection.Qty; //Decrease the QtyOnHand in the products table by Qty
                                selection.item.QtySold = selection.Qty;     //QtySold = Qty
                                selection.item.QtyOrdered = selection.Qty;  // QtyOrdered = Qty
                                selection.item.QtyBackOrdered = 0;          // QtyBackOrdered = 0 in the line items table
                            }
                            else
                            {
                                product.QtyOnHand = 0;                      //Decrease the QtyOnHand to 0 in the products table
                                selection.item.QtyBackOrdered += selection.Qty - product.QtyOnHand;  // Increase the QtyOnBackOrdered by the difference between Qty
                                                                                                     // and QtyOnHand in the products table
                                selection.item.QtySold = product.QtyOnHand;                          // QtySold = QtyOnHand
                                selection.item.QtyOrdered = selection.Qty;                           // QtyOrdered = Qty
                                selection.item.QtyBackOrdered = selection.Qty - product.QtyOnHand;   // QtyBackOrdered = Qty - QtyOnHand
                            }
                        }
                        _db.Orders.Add(order);
                        _db.SaveChanges();
                        // then add each item to the orderitems table
                        //foreach (OrderSelectionHelper selection in selections)
                        //{
                        //    OrderLineItem tItem = new OrderLineItem();
                        //    tItem. = selection.Qty;
                        //    tItem.ProductItemId = selection.item.Id;
                        //    tItem.OrderId = order.Id;
                        //    _db.OrderLineItems.Add(tItem);
                        //    _db.SaveChanges();
                        //}
                        // test trans by uncommenting out these 3 lines
                        //int x = 1;
                        //int y = 0;
                        //x = x / y;
                        _trans.Commit();
                        orderId = order.Id;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        _trans.Rollback();
                    }
                }
            }
            return orderId;
        }
    }
}