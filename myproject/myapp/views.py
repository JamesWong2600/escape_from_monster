from django.shortcuts import render
from django.http import JsonResponse
from .models import Player
from django.views.decorators.csrf import csrf_exempt
# Create your views here.
import json

@csrf_exempt
def register(request):
    if request.method == 'POST':
        data = json.loads(request.body)
        username = data.get('username')
        password = data.get('password')
        re_type_password = data.get('re_type_password')

        print(f"Username: {username}, Password: {password}, Re-type Password: {re_type_password}")
        if password != re_type_password:
            return JsonResponse({
                'message': 'Passwords do not match'
            }, status=300)
        
        if len(password) < 8:
            return JsonResponse({
                'message': 'Passwords must longer than 8 characters'
            }, status=400)

        # Save the user to the database
        try:
            user = Player.objects.create(username=username, password=password)
            return JsonResponse({
                'message': 'User registered successfully',
                'username': username
            })
        except Exception as e:
            return JsonResponse({
                'message': 'Error registering user',
                'error': str(e)
            }, status=500)
    
@csrf_exempt
def login(request):
    if request.method == 'POST':
        data = json.loads(request.body)
        username = data.get('username')
        password = data.get('password')


        user = Player.objects.filter(username=username, password=password).first()

        if user:
            return JsonResponse({
                'message': 'User logged in successfully',
                'username': username
            },status=200)
        else:
            return JsonResponse({
                'message': 'Invalid username or password'
            }, status=400)
        
@csrf_exempt
def get_player_scores(request):
    if request.method == 'GET':
        players = Player.objects.all().order_by('-scores')
        player_scores = []
        for player in players:
            player_scores.append({
                'username': player.username,
                'scores': player.scores
            })
        return JsonResponse({
            'player_scores': player_scores
        }, status=200)

@csrf_exempt
def update_player_scores(request):
    if request.method == 'POST':
        data = json.loads(request.body)
        username = data.get('username')
        scores = data.get('scores')

        # Update or create the player record
        player, created = Player.objects.update_or_create(
            username=username,
            defaults={'scores': scores}
        )

        if not created:
            return JsonResponse({
                'message': 'Player scores updated successfully',
                'username': username,
                'scores': player.scores
            }, status=200)
        else:
            return JsonResponse({
                'message': 'Player created successfully',
                'username': username,
                'scores': player.scores
            }, status=201)