from django.db import models

# Create your models here.
class Player(models.Model):
    id = models.AutoField(primary_key=True)  # Auto-incrementing ID
    username = models.CharField(max_length=150, unique=True)  # Unique username
    password = models.CharField(max_length=128)  # Password field
    scores = models.IntegerField(default=0)