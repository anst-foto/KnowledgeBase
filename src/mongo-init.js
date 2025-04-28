db.createUser({
    user: "user",
    pwd: "password",
    roles: [{
        role: "readWrite",
        db: "knowledge_base"
    }]
});
