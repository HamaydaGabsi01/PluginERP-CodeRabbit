using System;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace ETAT_READ
{
    public class BookingReadmissionTableAdapter
    {
        private readonly string _connectionString;

        public BookingReadmissionTableAdapter()
        {
            _connectionString = AtooERP.Network_setting.getConnectionString();
        }

        public BookingReadmissionDataSet GetData(int seasonId)
        {
            var dataset = new BookingReadmissionDataSet();
            
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                
                using (var command = new MySqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"
                        SELECT
                            ba.id AS Id,
                            old_res.id AS OldReservation,
                            old_res.guest AS Guest,
                            COALESCE(room1.name, e1.name) AS Room,
                            COALESCE(room2.name, e2.name) AS Room2,
                            new_res.id AS NewReservation,
                            new_res.memo AS Note,
                            new_res.application_id AS ApplicationId,
                            ap.first_name AS FirstName,
                            ap.last_name AS LastName,
                            CASE WHEN DOC01.check = 1 THEN 'Oui' ELSE 'Non' END AS CheckInscription,
                            ac.contact_value AS ContactValue,
                            CASE WHEN DOC02.check = 1 THEN 'Oui' ELSE 'Non' END AS CheckBudget,
                            brs.name AS State,
                            new_res.begin_date AS BeginDate,
                            new_res.end_date AS EndDate,
                            ROUND(tm.conteur) AS Counteur,
                            old_res.final_departure_date AS DD,
                            finalDepatureDoc.`check` AS PDD,
                            COALESCE(ua.restAmount, 0) as Impaye,
                            bg.profile_id AS ProfileId,
                            atooerp_domain.name AS Domain
                        FROM booking_reservation old_res
                        INNER JOIN booking_admission ba ON old_res.id = ba.reservation and ba.season = 25
                        INNER JOIN booking_guest bg ON bg.id = old_res.guest
                        LEFT JOIN (
                            SELECT 
                                br.Id,
                                br.guest,
                                br.begin_date,
                                br.end_date,
                                br.room,
                                br.memo,
                                br.reservation_state,
                                br.residence_out,
                                br.application_id,
                                br.final_departure_date
                            FROM booking_reservation br 
                            WHERE br.begin_date >= '2025-09-01'
                        ) new_res 
                            ON new_res.guest = old_res.guest
                            AND new_res.begin_date > old_res.end_date
                        LEFT JOIN booking_room room1 ON room1.id = old_res.room
                        LEFT JOIN booking_room room2 ON room2.id = new_res.room
                        INNER JOIN atooerp_person ap ON ap.id = bg.id
                        LEFT JOIN booking_reservation_state brs ON brs.id = new_res.reservation_state
                        LEFT JOIN atooerp_document DOC01 ON DOC01.piece = new_res.id AND DOC01.type_document = 2
                        LEFT JOIN atooerp_document DOC02 ON DOC02.piece = new_res.id AND DOC02.type_document = 9
                        LEFT JOIN atooerp_establishment e1 ON e1.id = old_res.residence_out
                        LEFT JOIN atooerp_establishment e2 ON e2.id = new_res.residence_out
                        LEFT JOIN atooerp_contact ac 
                            ON ac.person = ap.id
                            AND ac.contact_description IN (3,4) 
                            AND ac.`default` = 1
                        LEFT JOIN (
                            SELECT 
                                guest,
                                SUM(TIMESTAMPDIFF(DAY, begin_date, end_date)) / 30 AS conteur
                            FROM booking_reservation br
                            GROUP BY guest
                        ) tm ON tm.guest = old_res.guest
                        LEFT JOIN (
                            SELECT 
                                guest,
                                SUM(restAmount) as restAmount
                            FROM (
                                SELECT 
                                    ss.Id,
                                    ss.total_amount,
                                    ss.paied_amount,
                                    ss.total_amount - ss.paied_amount AS restAmount,
                                    ss.due_date,
                                    guest
                                FROM sale_shipping ss
                                INNER JOIN booking_reservation_period brp ON ss.Id = brp.sale_shipping
                                INNER JOIN booking_reservation br ON br.Id = reservation
                                WHERE ss.validated = 1
                                    AND ss.due_date <= IFNULL(NULL, CURRENT_DATE() + INTERVAL 1 DAY)
                                    AND ss.total_amount <> ss.paied_amount
                            ) req
                            GROUP BY guest
                        ) ua ON ua.guest = old_res.guest
                         LEFT OUTER JOIN
                                        (SELECT piece, MAX(`check`) AS `check`
                                         FROM      atooerp_document
                                         WHERE   (type_document = 1000)
                                         GROUP BY piece) finalDepatureDoc ON finalDepatureDoc.piece = old_res.Id 
                        LEFT OUTER JOIN atooerp_domain ON ap.primary_occupation = atooerp_domain.Id
                        WHERE old_res.guest IS NOT NULL
                        GROUP BY old_res.id, old_res.guest
                        ORDER BY ap.last_name";

                    command.Parameters.AddWithValue("@seasonId", seasonId);

                    using (var adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dataset, "BookingReadmission");
                    }
                }
            }

            return dataset;
        }

        public DataTable GetDataTable(int seasonId)
        {
            return GetData(seasonId).Tables["BookingReadmission"];
        }
    }
} 