# notable

application built using .NET6

to run this application, navigate into the notable.api directory and run cmd `dotnet run notable.api`

interactive swagger UI is available here: https://localhost:7167/swagger/index.html

endpoints include:
  - GET all doctors `https://localhost:7167/doctors`
  - GET all appointments
    - by doctor `https://localhost:7167/doctors/{doctorId}/appointments`
    - by doctor AND day `https://localhost:7167/doctors/{doctorId}/appointments/{day}` (day parameter needs to be in yyyy-mm-dd format)
  - POST new doctor `https://localhost:7167/doctors/{doctorId}`
    example request body:
    `{
        "firstName": "Nick",
        "lastName": "Riviera"
     }`
  - POST new appointment for a given doctor `https://localhost:7167/doctors/{doctorId}/appointments`
      example request body:
    `{
      "patient": {
        "firstName": "Jane",
        "lastName": "Doe"
      },
      "kind": "Follow-up",
      "visit": {
        "date": "2023-03-01",
        "time": "14:00:00"
      }
    }`
  - DELETE appointment for a given doctor `https://localhost:7167/doctors/{doctorId}/appointments/{appointmetId}`
  
  models are defined as:
    
    Doctor
      - id (some GUID converted to string)
      - first name
      - last name
    Appointment
      - id (some GUID converted to string)
      - doctorId (matches w id in Doctor)
      - patient
        - first name
        - last name
      - kind
      - visit
        - time
        - date
