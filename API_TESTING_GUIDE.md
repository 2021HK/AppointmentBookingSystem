# API Testing Guide - Step by Step

## 🔐 Step 1: Login (Get Token)

**Endpoint:** `POST /api/Auth/login`

### Test 1: Admin Login
```json
POST http://localhost:5000/api/Auth/login
Content-Type: application/json

{
  "username": "admin",
  "password": "password123"
}
```

**Expected Response (200):**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "username": "admin",
  "role": "Admin"
}
```

**✅ Copy the token - aapko baaki APIs mein use karna hai!**

---

### Test 2: User Login
```json
POST http://localhost:5000/api/Auth/login

{
  "username": "user1",
  "password": "password123"
}
```

### Test 3: Doctor Login
```json
POST http://localhost:5000/api/Auth/login

{
  "username": "dr.smith",
  "password": "password123"
}
```

### Test 4: Wrong Password (Should Fail)
```json
POST http://localhost:5000/api/Auth/login

{
  "username": "admin",
  "password": "wrongpassword"
}
```

**Expected Response (401):**
```json
{
  "message": "Invalid username or password"
}
```

---

## 👨‍⚕️ Step 2: Doctor APIs (Admin Only)

**⚠️ Important:** Pehle Admin token copy karo aur Authorization header mein lagao!

### Test 5: Get All Doctors
```
GET http://localhost:5000/api/Doctor
Authorization: Bearer YOUR_ADMIN_TOKEN_HERE
```

**Expected Response (200):**
```json
[
  {
    "id": 1,
    "name": "Dr. John Smith",
    "specialization": "Cardiology",
    "email": "dr.smith@hospital.com",
    "phone": "555-0101",
    "userId": 4,
    "createdAt": "2024-03-23T..."
  },
  {
    "id": 2,
    "name": "Dr. Sarah Jones",
    "specialization": "Neurology",
    "email": "dr.jones@hospital.com",
    "phone": "555-0102",
    "userId": 5,
    "createdAt": "2024-03-23T..."
  }
]
```

---

### Test 6: Get Doctor by ID
```
GET http://localhost:5000/api/Doctor/1
Authorization: Bearer YOUR_ADMIN_TOKEN_HERE
```

**Expected Response (200):**
```json
{
  "id": 1,
  "name": "Dr. John Smith",
  "specialization": "Cardiology",
  "email": "dr.smith@hospital.com",
  "phone": "555-0101",
  "userId": 4,
  "createdAt": "2024-03-23T..."
}
```

---

### Test 7: Create New Doctor (Admin Only)
```json
POST http://localhost:5000/api/Doctor
Authorization: Bearer YOUR_ADMIN_TOKEN_HERE
Content-Type: application/json

{
  "name": "Dr. Michael Brown",
  "specialization": "Orthopedics",
  "email": "dr.brown@hospital.com",
  "phone": "555-0103"
}
```

**Expected Response (200):**
```json
{
  "id": 3,
  "message": "Doctor created successfully"
}
```

---

### Test 8: Update Doctor (Admin Only)
```json
PUT http://localhost:5000/api/Doctor/3
Authorization: Bearer YOUR_ADMIN_TOKEN_HERE
Content-Type: application/json

{
  "name": "Dr. Michael Brown",
  "specialization": "Orthopedic Surgery",
  "email": "dr.brown@hospital.com",
  "phone": "555-0103"
}
```

**Expected Response (200):**
```json
{
  "message": "Doctor updated successfully"
}
```

---

### Test 9: Delete Doctor (Admin Only)
```json
DELETE http://localhost:5000/api/Doctor/3
Authorization: Bearer YOUR_ADMIN_TOKEN_HERE
```

**Expected Response (200):**
```json
{
  "message": "Doctor deleted successfully"
}
```

---

### Test 10: Try Doctor API without Token (Should Fail)
```
GET http://localhost:5000/api/Doctor
```

**Expected Response (401 Unauthorized)**

---

### Test 11: Try Doctor API with User Token (Should Fail)
```
GET http://localhost:5000/api/Doctor
Authorization: Bearer USER_TOKEN_HERE
```

**Expected Response (403 Forbidden)**

---

## 📅 Step 3: Slot APIs (Admin Only)

### Test 12: Create Slot for Doctor 1
```json
POST http://localhost:5000/api/Slot
Authorization: Bearer YOUR_ADMIN_TOKEN_HERE
Content-Type: application/json

{
  "doctorId": 1,
  "startTime": "2026-03-25T09:00:00",
  "endTime": "2026-03-25T09:30:00"
}
```

**Expected Response (200):**
```json
{
  "id": 1,
  "message": "Slot created successfully"
}
```

---

### Test 13: Create Multiple Slots
```json
POST http://localhost:5000/api/Slot

{
  "doctorId": 1,
  "startTime": "2026-03-25T09:30:00",
  "endTime": "2026-03-25T10:00:00"
}
```

```json
POST http://localhost:5000/api/Slot

{
  "doctorId": 1,
  "startTime": "2026-03-25T10:00:00",
  "endTime": "2026-03-25T10:30:00"
}
```

```json
POST http://localhost:5000/api/Slot

{
  "doctorId": 2,
  "startTime": "2026-03-25T14:00:00",
  "endTime": "2026-03-25T14:30:00"
}
```

---

### Test 14: Get Available Slots for Doctor
```
GET http://localhost:5000/api/Slot/doctor/1
Authorization: Bearer YOUR_ADMIN_TOKEN_HERE
```

**Expected Response (200):**
```json
[
  {
    "id": 1,
    "doctorId": 1,
    "startTime": "2026-03-25T09:00:00",
    "endTime": "2026-03-25T09:30:00",
    "isAvailable": true,
    "createdAt": "2024-03-23T..."
  },
  {
    "id": 2,
    "doctorId": 1,
    "startTime": "2026-03-25T09:30:00",
    "endTime": "2026-03-25T10:00:00",
    "isAvailable": true,
    "createdAt": "2024-03-23T..."
  }
]
```

---

### Test 15: Delete Slot
```
DELETE http://localhost:5000/api/Slot/1
Authorization: Bearer YOUR_ADMIN_TOKEN_HERE
```

**Expected Response (200):**
```json
{
  "message": "Slot deleted successfully"
}
```

---

## 📝 Step 4: Appointment APIs (User Role)

**⚠️ Important:** Ab User ka token use karo!

### Test 16: Book Appointment (User)
```json
POST http://localhost:5000/api/Appointment
Authorization: Bearer YOUR_USER_TOKEN_HERE
Content-Type: application/json

{
  "slotId": 2,
  "patientName": "John Doe",
  "patientEmail": "john.doe@example.com",
  "patientPhone": "555-1234",
  "notes": "First time visit"
}
```

**Expected Response (200):**
```json
{
  "id": 1,
  "message": "Appointment booked successfully"
}
```

---

### Test 17: Get My Appointments (User)
```
GET http://localhost:5000/api/Appointment/my-appointments
Authorization: Bearer YOUR_USER_TOKEN_HERE
```

**Expected Response (200):**
```json
[
  {
    "id": 1,
    "slotId": 2,
    "doctorName": "Dr. John Smith",
    "specialization": "Cardiology",
    "startTime": "2026-03-25T09:30:00",
    "endTime": "2026-03-25T10:00:00",
    "patientName": "John Doe",
    "patientEmail": "john.doe@example.com",
    "patientPhone": "555-1234",
    "status": "Booked",
    "notes": "First time visit",
    "createdAt": "2024-03-23T..."
  }
]
```

---

### Test 18: Cancel Appointment (User)
```
PUT http://localhost:5000/api/Appointment/1/cancel
Authorization: Bearer YOUR_USER_TOKEN_HERE
```

**Expected Response (200):**
```json
{
  "message": "Appointment cancelled successfully"
}
```

---

### Test 19: Try to Book Same Slot Again (Should Fail)
```json
POST http://localhost:5000/api/Appointment
Authorization: Bearer YOUR_USER_TOKEN_HERE

{
  "slotId": 2,
  "patientName": "Jane Doe",
  "patientEmail": "jane@example.com",
  "patientPhone": "555-5678"
}
```

**Expected Response (400):**
```json
{
  "error": "Slot is not available"
}
```

---

## 👨‍⚕️ Step 5: Doctor Schedule APIs (Doctor Role)

**⚠️ Important:** Ab Doctor ka token use karo!

### Test 20: Get My Schedule (Doctor)
```
GET http://localhost:5000/api/Appointment/my-schedule
Authorization: Bearer YOUR_DOCTOR_TOKEN_HERE
```

**Expected Response (200):**
```json
[
  {
    "id": 1,
    "slotId": 2,
    "doctorName": "Dr. John Smith",
    "specialization": "Cardiology",
    "startTime": "2026-03-25T09:30:00",
    "endTime": "2026-03-25T10:00:00",
    "patientName": "John Doe",
    "patientEmail": "john.doe@example.com",
    "patientPhone": "555-1234",
    "status": "Booked",
    "notes": "First time visit",
    "createdAt": "2024-03-23T..."
  }
]
```

---

## 🧪 Step 6: Validation Tests

### Test 21: Invalid Login Data
```json
POST http://localhost:5000/api/Auth/login

{
  "username": "",
  "password": ""
}
```

**Expected Response (400):**
```json
{
  "errors": {
    "Username": ["The Username field is required."],
    "Password": ["The Password field is required."]
  }
}
```

---

### Test 22: Invalid Doctor Data
```json
POST http://localhost:5000/api/Doctor
Authorization: Bearer YOUR_ADMIN_TOKEN_HERE

{
  "name": "",
  "specialization": "",
  "email": "invalid-email",
  "phone": ""
}
```

**Expected Response (400):**
```json
{
  "errors": {
    "Name": ["The Name field is required."],
    "Email": ["The Email field is not a valid e-mail address."]
  }
}
```

---

### Test 23: Invalid Appointment Data
```json
POST http://localhost:5000/api/Appointment
Authorization: Bearer YOUR_USER_TOKEN_HERE

{
  "slotId": 0,
  "patientName": "",
  "patientEmail": "invalid",
  "patientPhone": ""
}
```

**Expected Response (400):**
```json
{
  "errors": {
    "PatientName": ["The PatientName field is required."],
    "PatientEmail": ["The PatientEmail field is not a valid e-mail address."]
  }
}
```

---

## 📊 Testing Summary Checklist

### Authentication (Auth Controller)
- [ ] Test 1: Admin login ✓
- [ ] Test 2: User login ✓
- [ ] Test 3: Doctor login ✓
- [ ] Test 4: Wrong password ✓

### Doctor Management (Admin Only)
- [ ] Test 5: Get all doctors ✓
- [ ] Test 6: Get doctor by ID ✓
- [ ] Test 7: Create doctor ✓
- [ ] Test 8: Update doctor ✓
- [ ] Test 9: Delete doctor ✓
- [ ] Test 10: Unauthorized access ✓
- [ ] Test 11: Forbidden access (User role) ✓

### Slot Management (Admin Only)
- [ ] Test 12: Create slot ✓
- [ ] Test 13: Create multiple slots ✓
- [ ] Test 14: Get available slots ✓
- [ ] Test 15: Delete slot ✓

### Appointment Booking (User Role)
- [ ] Test 16: Book appointment ✓
- [ ] Test 17: Get my appointments ✓
- [ ] Test 18: Cancel appointment ✓
- [ ] Test 19: Book unavailable slot ✓

### Doctor Schedule (Doctor Role)
- [ ] Test 20: Get my schedule ✓

### Validation Tests
- [ ] Test 21: Invalid login data ✓
- [ ] Test 22: Invalid doctor data ✓
- [ ] Test 23: Invalid appointment data ✓

---

## 🎯 Quick Test Order

1. **Login as Admin** → Copy token
2. **Create Doctors** (Test 7)
3. **Create Slots** (Test 12, 13)
4. **Login as User** → Copy token
5. **Book Appointment** (Test 16)
6. **Check My Appointments** (Test 17)
7. **Login as Doctor** → Copy token
8. **Check My Schedule** (Test 20)

---

## 💡 Tips

1. **Token expire hota hai** - Agar 401 error aaye to phir se login karo
2. **Swagger use karo** - http://localhost:5000/swagger (easy testing)
3. **Postman use karo** - Collection already hai `postman/` folder mein
4. **Database check karo** - SSMS mein tables dekho data verify karne ke liye

---

**Happy Testing! 🚀**
